/*  ___   ___  ___  _   _  ___   ___   ____ ___  ____  
 * / _ \ /___)/ _ \| | | |/ _ \ / _ \ / ___) _ \|    \ 
 *| |_| |___ | |_| | |_| | |_| | |_| ( (__| |_| | | | |
 * \___/(___/ \___/ \__  |\___/ \___(_)____)___/|_|_|_|
 *                  (____/ 
 * Arduino Mecanum Omni Direction Wheel Robot Car
 * Tutorial URL https://osoyoo.com/?p=49235
 * CopyRight www.osoyoo.com

 * After running the code, smart car will 
 * go forward and go backward for 2 seconds, 
 * left turn and right turn for 2 seconds, 
 * right shift and left shift for 2 seconds,
 * left diagonal back and right diagonal forward for 2 seconds,
 * left diagonal forward and right diagonal back for 2 seconds,
 * then stop. 
 * 
 */
#define SPEED 25    
#define TURN_SPEED 25 

#define speedPinR 52   //  Front Wheel PWM pin connect Model-Y M_B ENA 
#define RightMotorDirPin1  50    //Front Right Motor direction pin 1 to Model-Y M_B IN1  (K1)
#define RightMotorDirPin2  48   //Front Right Motor direction pin 2 to Model-Y M_B IN2   (K1)                                 
#define LeftMotorDirPin1  46    //Front Left Motor direction pin 1 to Model-Y M_B IN3 (K3)
#define LeftMotorDirPin2  44   //Front Left Motor direction pin 2 to Model-Y M_B IN4 (K3)
#define speedPinL 42   //  Front Wheel PWM pin connect Model-Y M_B ENB

#define speedPinRB 32   //  Rear Wheel PWM pin connect Left Model-Y M_A ENA 
#define RightMotorDirPin1B  30    //Rear Right Motor direction pin 1 to Model-Y M_A IN1 ( K1)
#define RightMotorDirPin2B 28    //Rear Right Motor direction pin 2 to Model-Y M_A IN2 ( K1) 
#define LeftMotorDirPin1B 26    //Rear Left Motor direction pin 1 to Model-Y M_A IN3  (K3)
#define LeftMotorDirPin2B 24  //Rear Left Motor direction pin 2 to Model-Y M_A IN4 (K3)
#define speedPinLB 22    //  Rear Wheel PWM pin connect Model-Y M_A ENB


/*motor control*/
void go_advance(int speed){
   FL_fwd(speed); 
   FR_fwd(speed);
   RL_fwd(speed);
   RR_fwd(speed);

}
void go_back(int speed){
   FL_bck(speed); 
   FR_bck(speed);
   RL_bck(speed);
   RR_bck(speed);

}
void right_shift(int speed_fl_fwd,int speed_rl_bck ,int speed_rr_fwd,int speed_fr_bck) {
  FL_fwd(speed_fl_fwd); 
  FR_bck(speed_fr_bck);
  RL_bck(speed_rl_bck); 
  RR_fwd(speed_rr_fwd);

}
void left_shift(int speed_fl_bck,int speed_rl_fwd ,int speed_rr_bck,int speed_fr_fwd){
   FL_bck(speed_fl_bck);
   FR_fwd(speed_fr_fwd);
   RL_fwd(speed_rl_fwd);
   RR_bck(speed_rr_bck);
}

void left_turn(int speed){
   FL_bck(0); 
   FR_fwd(speed);
   RL_bck(0);
   RR_fwd(speed);
}
void right_turn(int speed){
  //  FL_fwd(speed); 
  //  FR_bck(0);
  //  RL_fwd(speed);
  //  RR_bck(0);

   FL_fwd(speed); 
   FR_fwd(0);
   RL_fwd(speed);
   RR_fwd(0);

}
void left_back(int speed){
   FL_fwd(0); 
   FR_bck(speed);
   RL_fwd(0);
   RR_bck(speed);

}
void right_back(int speed){
   FL_bck(speed); 
   FR_fwd(0);
   RL_bck(speed);
   RR_fwd(0);

 
}
void clockwise(int speed){
   FL_fwd(speed); 
   FR_bck(speed);
   RL_fwd(speed);
   RR_bck(speed);
}
void countclockwise(int speed){
   FL_bck(speed); 
   FR_fwd(speed);
   RL_bck(speed);
   RR_fwd(speed);
}

void FL_fwd(int speed) // front-left wheel forward turn
{
  digitalWrite(LeftMotorDirPin1,HIGH);
  digitalWrite(LeftMotorDirPin2,LOW);
  analogWrite(speedPinL,speed);
}
void FL_bck(int speed) // front-left wheel backward turn
{
  digitalWrite(LeftMotorDirPin1,LOW);
  digitalWrite(LeftMotorDirPin2,HIGH);
  analogWrite(speedPinL,speed);
}

void FR_fwd(int speed)  //front-right wheel forward turn
{
  digitalWrite(RightMotorDirPin1,HIGH);
  digitalWrite(RightMotorDirPin2,LOW); 
  analogWrite(speedPinR,speed);
}
void FR_bck(int speed) // front-right wheel backward turn
{
  digitalWrite(RightMotorDirPin1,LOW);
  digitalWrite(RightMotorDirPin2,HIGH); 
  analogWrite(speedPinR,speed);
}

void RL_fwd(int speed)  //rear-left wheel forward turn
{
  digitalWrite(LeftMotorDirPin1B,HIGH);
  digitalWrite(LeftMotorDirPin2B,LOW);
  analogWrite(speedPinLB,speed);
}
void RL_bck(int speed)    //rear-left wheel backward turn
{
  digitalWrite(LeftMotorDirPin1B,LOW);
  digitalWrite(LeftMotorDirPin2B,HIGH);
  analogWrite(speedPinLB,speed);
}

void RR_fwd(int speed)  //rear-right wheel forward turn
{
  digitalWrite(RightMotorDirPin1B, HIGH);
  digitalWrite(RightMotorDirPin2B,LOW); 
  analogWrite(speedPinRB,speed);
}
void RR_bck(int speed)  //rear-right wheel backward turn
{
  digitalWrite(RightMotorDirPin1B, LOW);
  digitalWrite(RightMotorDirPin2B,HIGH); 
  analogWrite(speedPinRB,speed);
}

 
void stop_Stop()    //Stop
{
  analogWrite(speedPinLB,0);
  analogWrite(speedPinRB,0);
  analogWrite(speedPinL,0);
  analogWrite(speedPinR,0);
}


//Pins initialize
void init_GPIO()
{
  pinMode(RightMotorDirPin1, OUTPUT); 
  pinMode(RightMotorDirPin2, OUTPUT); 
  pinMode(speedPinL, OUTPUT);  
 
  pinMode(LeftMotorDirPin1, OUTPUT);
  pinMode(LeftMotorDirPin2, OUTPUT); 
  pinMode(speedPinR, OUTPUT);
  
  pinMode(RightMotorDirPin1B, OUTPUT); 
  pinMode(RightMotorDirPin2B, OUTPUT); 
  pinMode(speedPinLB, OUTPUT);  
 
  pinMode(LeftMotorDirPin1B, OUTPUT);
  pinMode(LeftMotorDirPin2B, OUTPUT);
  pinMode(speedPinRB, OUTPUT);
   
  stop_Stop();
}

void setup()
{
  init_GPIO();
  delay(4000);
 
go_advance(SPEED);
     delay(800);
     stop_Stop();
     delay(800);
  
go_back(SPEED);
      delay(800);
      stop_Stop();
      delay(800);
	  
left_turn(TURN_SPEED);
      delay(800);
      stop_Stop();
      delay(800);
	  
right_turn(TURN_SPEED);
     delay(800);
     stop_Stop();
     delay(800);
	   
left_shift(200,200,200,200); //left shift
     delay(800);
     stop_Stop();
     delay(800);	
	 
right_shift(200,200,200,200); //right shift
     delay(800);
     stop_Stop();
     delay(800);
	  
right_shift(200,0,200,0); //right diagonal ahead
     delay(800);
     stop_Stop();
	 delay(800);	 
	 
left_shift(200,0,200,0); //left diagonal back
     delay(800);
     stop_Stop();
	 delay(800);
 
left_shift(0,200,0,200); //left diagonal ahead
     delay(800);
     stop_Stop();
     delay(800);

right_shift(0,200,0,200); //right diagonal back
     delay(800);
     stop_Stop();
	 delay(800);
}

void loop(){
}
