#some buttons may not work

import pygame, random, time

pygame.init()



def battle1():
	


	import pygame, random, time

	run = True

	pygame.init()
	pygame.font.init()


	class Button():
		def __init__(self, x, y, image, scale):
			width = image.get_width()
			height = image.get_height()
			self.image = pygame.transform.scale(image, (int(width*scale), int(height*scale)))
			self.rect = self.image.get_rect()
			self.rect.topleft = (x,y)
			self.clicked = False

		def draw(self, surface):
			action = False
			#mouse pos
			pos = pygame.mouse.get_pos()

			#checks mouseover and clicked conditions
			if self.rect.collidepoint(pos):
				if pygame.mouse.get_pressed()[0] == 1 and self.clicked == False:
					self.clicked = True
					action = True

			if pygame.mouse.get_pressed()[0] == 0:
				self.clicked = False


			#draw button on screen
			surface.blit(self.image, (self.rect.x, self.rect.y))

			return action

	#potion button
	potionbuttonimage = pygame.image.load("Images/Panel/potionbutton.png")
	potionbuttonimage = pygame.transform.scale(potionbuttonimage, (100, 100))
	potionbutton = Button(280, 505, potionbuttonimage, 1)


	#loads restart image
	restart_image = pygame.image.load("Images/BattleEnd/Restart.png")
	restart_image = pygame.transform.scale(restart_image, (230,80))
	restartbutton = Button(215, 145, restart_image, 1)

	#loads return image
	return_image = pygame.image.load("Images/BattleEnd/Return.png")
	return_image = pygame.transform.scale(return_image, (230,80))
	returnbutton = Button(215, 235, return_image, 1)


	#define colours
	red = (255, 0, 0)
	green = (0, 255, 0)
	black = (0, 0, 0)
	blue = (0, 150, 255)
	yellow = (215, 157, 36)
	purple = (180,0, 180)
	white = (255,255, 255)
	gold = (225, 160, 0)

		


	#define game variables
	current_fighter = 1
	total_fighters = 2
	action_cooldown = 0
	action_wait_time = 65
	attack = False
	potions = False
	clicked = False
	game_over = 0

	#framerate
	clock = pygame.time.Clock()
	fps = 60 #change back to 60

	#game window
	bottom_panel = 150
	screen_width = 640
	screen_height = 480 + bottom_panel



	screen = pygame.display.set_mode((screen_width, screen_height))
	pygame.display.set_caption('Battle')


	#define font
	pygame.font.init()
	font = pygame.font.SysFont('Modern', 40)



	#load images
	#background image
	background_image = pygame.image.load('Images/Background/newbackground.png').convert_alpha() 

	#panel image
	panel_image = pygame.image.load('Images/Panel/newpanel.png').convert_alpha()
	#enemy select mouse hover image
	sword_image = pygame.image.load('Images/Panel/sword.png').convert_alpha() 


	#function for text
	def draw_text(text, font, text_col, x, y):
		img = font.render(text, True, text_col)
		screen.blit(img, (x, y))


	#function for showing background
	def show_bg():
		screen.blit(background_image,(0, 0))

	#function for showing panel
	def show_panel():
		#draw rectangle panel
		screen.blit(panel_image,(0, screen_height - bottom_panel))
		#show fighter stats
		draw_text(f'{Hero.name} HP: {Hero.hp}', font, red, 50, screen_height - 600)
		for count, i in enumerate(bandit_list):
			#show name and health
			draw_text(f'{Bandit1.name} HP: {Bandit1.hp}', font, red, 400, screen_height - 600) 


	#fighter class
	class Fighter():
		def __init__(self, x, y, name, max_hp, strength, potions, level):
			self.name = name
			self.max_hp = max_hp
			self.hp = max_hp
			self.strength = strength
			self.start_potions = potions
			self.potions = potions
			self.alive = True
			self.animation_list = [] #to animate across the 7 images 
			self.frame_index = 1 #controls which picture as it goes on
			self.level = 1
			
			self.action = 0 #0 is idle, 1 is attack, 2 is dead ANIMATIONS
			
			self.update_time = pygame.time.get_ticks() #time

			#Loading all the different animations 

			#load IDLE images
			temp_list = []
			for i in range(8): #number of images it goes through
				img = pygame.image.load(f'Images/{self.name}/Idle/{i}.png') 
				img = pygame.transform.scale(img, (img.get_width() * 1.7, img.get_height() * 1.7)) #size 
				temp_list.append(img) #list with all the idle images
			self.animation_list.append(temp_list)

			#load ATTACK images
			temp_list = []
			for i in range(8): #number of images it goes through
				img = pygame.image.load(f'Images/{self.name}/Attack/{i}.png') 
				img = pygame.transform.scale(img, (img.get_width() * 1.7, img.get_height() * 1.7)) #size 
				temp_list.append(img) #list with all the attack images
			self.animation_list.append(temp_list)
			
			#load DEATH images
			temp_list = []
			for i in range(8): #number of images it goes through
				img = pygame.image.load(f'Images/{self.name}/Death/{i}.png') 
				img = pygame.transform.scale(img, (img.get_width() * 1, img.get_height() * 1)) #size
				img = pygame.transform.scale(img, (325, 275))
				temp_list.append(img) #list with all the death images
			self.animation_list.append(temp_list)


			self.image = self.animation_list[self.action][self.frame_index] #set to first start
			self.rect = self.image.get_rect() 
			self.rect.center = (x, y)


			#animation
		def update(self):
			animation_cooldown = 135
			#handle animation
			#update image
			self.image = self.animation_list[self.action][self.frame_index]
			#checks if enough time passed since last update
			if pygame.time.get_ticks() - self.update_time > animation_cooldown: #timer to update stages of animation
				self.update_time = pygame.time.get_ticks()
				self.frame_index += 1
			#if animation runs out then resets back to start
			if self.frame_index >= len(self.animation_list[self.action]):
				if self.action == 2:
					self.frame_index = len(self.animation_list[self.action]) - 1
				else:
					self.idle()

		#idle class
		def idle(self):
			#set variables to idle animation
			self.action = 0
			self.frame_index = 0
			self.update_time = pygame.time.get_ticks()


		def death(self):
			#set variables to death animation
			self.action = 2
			self.frame_index = 0
			self.update_time = pygame.time.get_ticks()

		#attack class
		def attack(self, target):
			#deal damage to enemy, slightly randomated
			rand = random.randint(-4, 5)
			damage = self.strength + rand
			target.hp -= damage
			#check if fighter has died
			if target.hp < 1:
				target.hp = 0
				target.alive = False
				target.death()
			#set variables to attack animation
			self.action = 1
			self.frame_index = 0
			self.update_time = pygame.time.get_ticks()

		def reset(self):
			self.alive = True
			self.potions = self.start_potions
			self.hp = self.max_hp
			self.frame_index = 0
			self.action = 0
			self.update_time = pygame.time.get_ticks()


		#show fighter image	
		def draw(self):
			screen.blit(self.image, self.rect)



	#healthbar class
	class HPBar():
		def __init__(self, x, y, hp, max_hp):
			self.x = x
			self.y = y
			self.hp = hp 
			self.max_hp = max_hp

			#the hp bars
		def draw(self, hp, max_hp):
			#update with new hp
			self.hp = hp
			self.max_hp = max_hp
			#calculate health ratio for green bar decrease 
			ratio = self.hp / self.max_hp
			pygame.draw.rect(screen, red, (self.x, self.y, 200, 20)) # red bar
			pygame.draw.rect(screen, green, (self.x, self.y, 200 * ratio, 20)) #green bar
		




	#CHARACTERS FROM THE FIGHTER CLASS-------------------------------------------------------------------------------
	Hero = Fighter(170, 335, 'Fighter', 100, 15, 2, 1) 
	Bandit1 = Fighter(475, 335, 'Bandit', 90, 20, 0, 1)


	#can be for multiple bandits
	bandit_list = []
	bandit_list.append(Bandit1)

	#draw hp bars
	hero_hpbar = HPBar(55, screen_height - 570, Hero.hp, Hero.max_hp)
	bandit1_hpbar = HPBar(400, screen_height - 570, Bandit1.hp, Bandit1.max_hp)


	run = True 
	while run:



		#framerate
		clock.tick(fps)

		#show background
		show_bg()

		#show panel
		show_panel()

		#show hpbars
		hero_hpbar.draw(Hero.hp, Hero.max_hp)
		bandit1_hpbar.draw(Bandit1.hp, Bandit1.max_hp)

		#show fighters
		Hero.update() #animation
		Hero.draw() #call image
		for bandit in bandit_list:
			bandit.update()
			bandit.draw()

		#control player actions
		#reset action variables
		attack = False
		potion = False
		target = None
		#makes mouse visible
		pygame.mouse.set_visible(True)
		#for the mouse over the enemy
		pos = pygame.mouse.get_pos()
		for count, bandit in enumerate(bandit_list):
			if bandit.rect.collidepoint(pos):
				#hide mouse over enemy
				pygame.mouse.set_visible(False)
				#show sword icon instead of mouse cursor
				screen.blit(sword_image, pos)
				if clicked == True and bandit.alive == True:
					attack = True
					target = bandit_list[count]
		

		if potionbutton.draw(screen):
			potion = True

		#Shows how many potions are left
		draw_text(str(Hero.potions), font, red, 358, 510)

		draw_text(f'Hero Level: {Hero.level}', font, red, 70, 85)
		


		if game_over == 0:
			#player action
			if Hero.alive == True:
				if current_fighter == 1:
					action_cooldown += 1
					if action_cooldown >= action_wait_time:
						#look for player action
						#attack
						if attack == True and target != None:
							Hero.attack(target)
							current_fighter += 1
							action_cooldown = 0
					if potion == True:
						if Hero.potions > 0:
							Hero.hp = Hero.hp + 40
							if Hero.hp > Hero.max_hp:
								Hero.hp = Hero.max_hp
							Hero.potions = Hero.potions -1
							current_fighter += 1
							action_cooldown = 0
			else:
				game_over = 1


			#enemy action
			for count, bandit in enumerate(bandit_list):
				if current_fighter == 2 + count:
					if Bandit1.alive == True:                       #---------------[]][][][][][]
						action_cooldown += 1
						if action_cooldown >= action_wait_time:
							#attack
							Bandit1.attack(Hero)
							current_fighter += 1
							action_cooldown = 0
					else:
						game_over = 2

				else:
					current_fighter += 0


		if game_over == 1:
			draw_text(str("You Lose :(((("), font, red, 240, 115)
			if restartbutton.draw(screen):
				Hero.reset()
				Bandit1.reset()
				game_over = 0
				
			if returnbutton.draw(screen):
				screen = pygame.display.set_mode((640, 480))
				run = False
				print("Return Pressed")


		if game_over == 2:
			draw_text(str("You Win! Hero Level +1"), font, red, 170, 115)
			if restartbutton.draw(screen):
				Hero.level = Hero.level + 1
				Hero.strength = Hero.strength + 5
				Hero.max_hp = Hero.max_hp + 10
				Hero.hp = Hero.hp + 10
			
				Hero.reset()
				Bandit1.reset()
				game_over = 0
				current_fighter += 1
				action_cooldown = 0
					


			if returnbutton.draw(screen):
				screen = pygame.display.set_mode((640, 480))
				run = False
				print("Return Pressed")


		#if fighters have their turn, rest
		if current_fighter > total_fighters:
			current_fighter = 1


		for event in pygame.event.get(): #this is the quit window stuff ================
			if event.type == pygame.QUIT:
				run = False
				battle = False
			if event.type == pygame.MOUSEBUTTONDOWN: #for the sword cursor clicks
				clicked = True
			else:
				clicked = False


		pygame.display.update()
		pygame.display.quit

		pygame.quit



screen_width = 640
screen_height = 480 

red = (255, 0, 0)
green = (0, 255, 0)
black = (0, 0, 0)
blue = (0, 150, 255)
yellow = (215, 157, 36)
purple = (180,0, 180)
white = (255,255, 255)
gold = (225, 160, 0)

screen = pygame.display.set_mode((screen_width, screen_height))
pygame.display.set_caption("Main Menu")
background_image = pygame.image.load('Images/Menu/MenuFrame.png').convert_alpha()
background_image = pygame.transform.scale(background_image,(640, 480))

#game variables imppoooorrrttannnntttt ----------
gamepaused = False
menustate = "main"

def show_bg():
		screen.blit(background_image,(0, 0))

font = pygame.font.SysFont("arial", 40)
textcol = (255, 255, 255)


#---------------------------------------------------

#buttons, second line for each changes size
resumebuttonimage = pygame.image.load("Images/Menu/resumebutton.png").convert_alpha()
resumebuttonimage = pygame.transform.scale(resumebuttonimage, (170, 110))

exitbuttonimage = pygame.image.load("Images/Menu/exitbutton.png").convert_alpha()
exitbuttonimage = pygame.transform.scale(exitbuttonimage, (150, 110))

ngbuttonimage = pygame.image.load("Images/Menu/ngbutton.png").convert_alpha()
ngbuttonimage = pygame.transform.scale(ngbuttonimage, (190, 110))

backbuttonimage = pygame.image.load("Images/Menu/backbutton.png").convert_alpha()
backbuttonimage = pygame.transform.scale(backbuttonimage, (60, 50))

zombiebuttonimage = pygame.image.load("Images/Menu/zombiebutton.png").convert_alpha()
zombiebuttonimage = pygame.transform.scale(zombiebuttonimage, (190, 100))

reaperbuttonimage = pygame.image.load("Images/Menu/reaperbutton.png").convert_alpha()
reaperbuttonimage = pygame.transform.scale(reaperbuttonimage, (190, 100)) 

#---------------------------------------------------

#button class
class Button():
	def __init__(self, x, y, image, scale):
		width = image.get_width()
		height = image.get_height()
		self.image = pygame.transform.scale(image, (int(width*scale), int(height*scale)))
		self.rect = self.image.get_rect()
		self.rect.topleft = (x,y)
		self.clicked = False

	def draw(self, surface):
		action = False
		#mouse pos
		pos = pygame.mouse.get_pos()

		#checks mouseover and clicked conditions
		if self.rect.collidepoint(pos):
			if pygame.mouse.get_pressed()[0] == 1 and self.clicked == False:
				self.clicked = True
				action = True

		if pygame.mouse.get_pressed()[0] == 0:
			self.clicked = False

		#draw button on screen
		surface.blit(self.image, (self.rect.x, self.rect.y))

		return action

#showing the buttons 
resumebutton = Button(230, 60, resumebuttonimage, 1)
exitbutton = Button(240, 300, exitbuttonimage, 1)
ngbutton = Button(220, 180, ngbuttonimage, 1)
backbutton = Button(65, 57, backbuttonimage, 1)
zombiebutton = Button(220, 100, zombiebuttonimage, 1)
reaperbutton = Button(220, 240, reaperbuttonimage, 1)


def drawtext(text, font, textcol, x, y):
	img = font.render(text, True, textcol)
	screen.blit(img, (x, y))

#game loop
run = True
while run:
	screen.fill((00,00,00))

	show_bg()


	if gamepaused == True:
		#then check menustate
		if menustate == "main":	
			if exitbutton.draw(screen):
				run = False
			if ngbutton.draw(screen):
				print("New Game Button Pressed")
				herolevel = 0 #for newgame the hero level will just go back to zero, each fight they gain exp and each level is a little more damage
				menustate = "enemyselect"
			#after level reset just do what resume would do, go to menu to select enemies
			if resumebutton.draw(screen):
				print("Resume Button Pressed")
				menustate = "enemyselect"
	
		#check menustate if the enemyselect menustate is on
		if menustate == "enemyselect":
			if zombiebutton.draw(screen):
				battle1()
				print("Zombie Button Pressed")
				
			
		
			if reaperbutton.draw(screen):
				print("Reaper Button Pressed")
			if backbutton.draw(screen): #for pressing back button
				menustate = "main"
	else:
		drawtext("Press Space To Begin", font, textcol, 160, 215)

	for event in pygame.event.get():
		if event.type == pygame.KEYDOWN:
			if event.key == pygame.K_SPACE:
				print("hi")
				gamepaused = True
		if event.type == pygame.QUIT:
			run = False

	pygame.display.update()

pygame.quit()