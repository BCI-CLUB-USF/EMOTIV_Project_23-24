#define speedPinR 10            //Front Wheel PWM pin connect Model-Y M_B ENA 
#define RightMotorDirPin1 50    // Front Right Motor direction pin 1 to Model-Y M_B IN1 (K1)
#define RightMotorDirPin2 48    // Front Right Motor direction pin 2 to Model-Y M_B IN2 (K1)                                 
#define LeftMotorDirPin1 46     // Front Left Motor direction pin 1 to Model-Y M_B IN3 (K3)
#define LeftMotorDirPin2 44     // Front Left Motor direction pin 2 to Model-Y M_B IN4 (K3)
#define speedPinL 8             // Front Wheel PWM pin connect Model-Y M_B ENB

#define speedPinRB 7            // Rear Wheel PWM pin connect Left Model-Y M_A ENA 
#define RightMotorDirPin1B 30   // Rear Right Motor direction pin 1 to Model-Y M_A IN1 (K1)
#define RightMotorDirPin2B 28   // Rear Right Motor direction pin 2 to Model-Y M_A IN2 (K1) 
#define LeftMotorDirPin1B 26    // Rear Left Motor direction pin 1 to Model-Y M_A IN3 (K3)
#define LeftMotorDirPin2B 24    // Rear Left Motor direction pin 2 to Model-Y M_A IN4 (K3)
#define speedPinLB 5            // Rear Wheel PWM pin connect Model-Y M_A ENB

#define LEDPin1 31              // Front Right Wheel
#define LEDPin2 33              // Front Left Wheel
#define LEDPin3 35              // Rear Right Wheel
#define LEDPin4 37              // Rear Left Wheel
#define LEDPin5 39              // Rear Right Turning Light
#define LEDPin6 41              // Rear Left Turning Light
#define LEDPin7 43              // Front Headlights
#define LEDPin8 45              // Rear Backlights

#define MAX_SPEED 63
#define ACCELERATION 3
#define DEACCELERATION 5
#define DELAY_TIME 30
#define COMMAND_TIMEOUT 200

unsigned long delayStart = 0;

char command = 0;
unsigned long commandStart = 0;
bool blinkerActive = false;

struct MotorSpeeds{
  int fr;
  int fl;
  int rr;
  int rl;
};

MotorSpeeds currentMSpeed = {0, 0, 0, 0};
MotorSpeeds targetMSpeed = {0, 0, 0, 0};

void lerp(int* a, int b, int alpha) {
  if (*a == b) {
    return;
  }

  if (*a < b) {
    int newValue = *a + alpha;
    
    if (newValue > b) {
      *a = b;
    } else {
      *a = newValue;
    }
  } else {
    int newValue = *a - alpha;
    
    if (newValue < b) {
      *a = b;
    } else {
      *a = newValue;
    }
  }
}

// ============== Initialization ==============

void initPins() {
  // Motors
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

  // LEDS
  pinMode(LEDPin1, OUTPUT);
  pinMode(LEDPin2, OUTPUT);
  pinMode(LEDPin3, OUTPUT);
  pinMode(LEDPin4, OUTPUT);
  pinMode(LEDPin5, OUTPUT);
  pinMode(LEDPin6, OUTPUT);
  pinMode(LEDPin7, OUTPUT);
  pinMode(LEDPin8, OUTPUT);
}

// ============= LED Functions =============

void powerLEDs() {
  digitalWrite(LEDPin1,HIGH);
  digitalWrite(LEDPin2,HIGH);
  digitalWrite(LEDPin3,HIGH);
  digitalWrite(LEDPin4,HIGH);
  digitalWrite(LEDPin5,HIGH);
  digitalWrite(LEDPin6,HIGH);
  digitalWrite(LEDPin7,HIGH);
  digitalWrite(LEDPin8,HIGH);
}

void modLEDFront() {
  digitalWrite(LEDPin5,LOW);
  digitalWrite(LEDPin6,LOW);
  digitalWrite(LEDPin7,HIGH);
  digitalWrite(LEDPin8,LOW);
}

void modLEDBack() {
  digitalWrite(LEDPin5,LOW);
  digitalWrite(LEDPin6,LOW);
  digitalWrite(LEDPin7,LOW);
  digitalWrite(LEDPin8,HIGH);
}

void blinkerLEDR() {
  if (!blinkerActive) {
    blinkerActive = true;
    delay(400);
    digitalWrite(LEDPin5,HIGH);
    delay(400);
    digitalWrite(LEDPin5,LOW);
  }
  blinkerActive = false;
}

void blinkerLEDL() {
  if (!blinkerActive) {
    blinkerActive = true;
    delay(400);
    digitalWrite(LEDPin6,HIGH);
    delay(400);
    digitalWrite(LEDPin6,LOW);
  }
  blinkerActive = false;
}

// ============= Boolean Functions =============

bool isStationary() {
  return currentMSpeed.fr == 0 && currentMSpeed.fl == 0 && currentMSpeed.rl == 0 && currentMSpeed.rr == 0;
}

// ============== Motor Pin Functions ==============

void FR_bck(int speed) {    // front-right wheel forward turn
  digitalWrite(RightMotorDirPin1, LOW);
  digitalWrite(RightMotorDirPin2,HIGH); 
  analogWrite(speedPinR,speed);
}
void FR_fwd(int speed) {    // front-right wheel backward turn
  digitalWrite(RightMotorDirPin1,HIGH);
  digitalWrite(RightMotorDirPin2,LOW); 
  analogWrite(speedPinR,speed);
}
void FL_bck(int speed) {    // front-left wheel forward turn
  digitalWrite(LeftMotorDirPin1,LOW);
  digitalWrite(LeftMotorDirPin2,HIGH);
  analogWrite(speedPinL,speed);
}
void FL_fwd(int speed) {    // front-left wheel backward turn
  digitalWrite(LeftMotorDirPin1,HIGH);
  digitalWrite(LeftMotorDirPin2,LOW);
  analogWrite(speedPinL,speed);
}
void RR_bck(int speed) {    // rear-right wheel forward turn
  digitalWrite(RightMotorDirPin1B, LOW);
  digitalWrite(RightMotorDirPin2B,HIGH); 
  analogWrite(speedPinRB,speed);
}
void RR_fwd(int speed) {    // rear-right wheel backward turn
  digitalWrite(RightMotorDirPin1B, HIGH);
  digitalWrite(RightMotorDirPin2B,LOW); 
  analogWrite(speedPinRB,speed);
}
void RL_bck(int speed) {    // rear-left wheel forward turn
  digitalWrite(LeftMotorDirPin1B,LOW);
  digitalWrite(LeftMotorDirPin2B,HIGH);
  analogWrite(speedPinLB,speed);
}
void RL_fwd(int speed) {    // rear-left wheel backward turn
  digitalWrite(LeftMotorDirPin1B,HIGH);
  digitalWrite(LeftMotorDirPin2B,LOW);
  analogWrite(speedPinLB,speed);
}

// ============== Motor Functions ==============

void updateCurrentMSpeed(int speedRate) {
  if (currentMSpeed.fr != targetMSpeed.fr) {
    lerp(&currentMSpeed.fr, targetMSpeed.fr, speedRate);

    if(currentMSpeed.fr < 0) {
      FR_bck(abs(currentMSpeed.fr));
    } else {
      FR_fwd(abs(currentMSpeed.fr));
    }
  }

  if (currentMSpeed.fl != targetMSpeed.fl) {
    lerp(&currentMSpeed.fl, targetMSpeed.fl, speedRate);
    
    if(currentMSpeed.fl < 0) {
      FL_bck(abs(currentMSpeed.fl));
    } else {
      FL_fwd(abs(currentMSpeed.fl));
    }
  }

  if (currentMSpeed.rl != targetMSpeed.rl) {
    lerp(&currentMSpeed.rl, targetMSpeed.rl, speedRate);
    
    if(currentMSpeed.rl < 0) {
      RL_bck(abs(currentMSpeed.rl));
    } else {
      RL_fwd(abs(currentMSpeed.rl));
    }
  }

  if (currentMSpeed.rr != targetMSpeed.rr) {
    lerp(&currentMSpeed.rr, targetMSpeed.rr, speedRate);
    
    if(currentMSpeed.rr < 0) {
      RR_bck(abs(currentMSpeed.rr));
    } else {
      RR_fwd(abs(currentMSpeed.rr));
    }
  }
}

void updateMSpeed() {
  if (millis() - delayStart >= DELAY_TIME) {
    delayStart += DELAY_TIME;
    
    switch(command) {
      case 'F':   // Move Forwards
        targetMSpeed.fr = MAX_SPEED;
        targetMSpeed.fl = MAX_SPEED;
        targetMSpeed.rl = MAX_SPEED;
        targetMSpeed.rr = MAX_SPEED;
        updateCurrentMSpeed(ACCELERATION);
        modLEDFront();
        break;
      
      case 'R':   // Shift Right
        targetMSpeed.fr = MAX_SPEED;
        targetMSpeed.fl = -MAX_SPEED;
        targetMSpeed.rr = MAX_SPEED;
        targetMSpeed.rl = -MAX_SPEED;
        updateCurrentMSpeed(ACCELERATION);
        break;

      case 'B':   // Move Backwards
        targetMSpeed.fr = -MAX_SPEED;
        targetMSpeed.fl = -MAX_SPEED;
        targetMSpeed.rl = -MAX_SPEED;
        targetMSpeed.rr = -MAX_SPEED;
        updateCurrentMSpeed(ACCELERATION);
        modLEDBack();
        break;

      case 'L':   // Shift Left
        targetMSpeed.fr = -MAX_SPEED;
        targetMSpeed.fl = MAX_SPEED;
        targetMSpeed.rr = -MAX_SPEED;
        targetMSpeed.rl = MAX_SPEED;
        updateCurrentMSpeed(ACCELERATION);
        break;

      case '<':   // Turn Left
        targetMSpeed.fr = 0;
        updateCurrentMSpeed(DEACCELERATION);
        blinkerLEDL();
        break;

      case '>':   // Turn Right
        targetMSpeed.fl = 0;
        updateCurrentMSpeed(DEACCELERATION);
        blinkerLEDR();
        break;
      
      case '1':
        break;

      default:
        if(!isStationary()) {
          targetMSpeed.fr = 0;
          targetMSpeed.fl = 0;
          targetMSpeed.rl = 0;
          targetMSpeed.rr = 0;
          updateCurrentMSpeed(DEACCELERATION);
        } else {
          powerLEDs();
        }
        break;
    }

    if (!Serial1.available() && command != '0') {
      command = '1';
    }
  }
}

// =============== MAIN FUNCTIONS ===============

void setup() {
  Serial.begin(9600);
  Serial1.begin(9600);

  delayStart = millis();

  initPins();

  powerLEDs();
}

void loop() {
  if (Serial1.available()) {
    size_t len = Serial1.available();
    uint8_t sbuf[len + 1];
    sbuf[len] = 0x00;
    Serial1.readBytes(sbuf, len);
    command = sbuf[0];
    commandStart = millis();
  }

  Serial.println(command);

  if (millis() - commandStart >= COMMAND_TIMEOUT) {
    command = '0';
  }

  updateMSpeed();

  // if (Serial.available()) {
  //   Serial1.write(Serial.read());
  // }
}