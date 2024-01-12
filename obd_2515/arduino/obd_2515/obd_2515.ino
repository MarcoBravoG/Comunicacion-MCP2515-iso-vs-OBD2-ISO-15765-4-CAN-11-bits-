/* OBD2 MCP2515 APP
 * By moty22.co.uk 
 * mcp_can lib written By: Cory J. Fowler  December 20th, 2016
 * https://github.com/coryjfowler/MCP_CAN_lib 
 */ 

#include <mcp_can.h>
#include <SPI.h>

#define standard 1  //standard is 11 bits

unsigned long rxId;
byte len;
byte rxBuf[8];
unsigned int a, b;
unsigned char back=0, line, Bmode, Bpid;

MCP_CAN CAN0(10);   // Set CAN0 CS to pin 10

void setup()
{
  Serial.begin(9600);
  while(!Serial);
  // Initialize MCP2515 running at baudrate of 500kb/s . If your can board has 16MHz crystal change the setting.  
  if(CAN0.begin(MCP_STDEXT, CAN_500KBPS, MCP_8MHZ) == CAN_OK)
    Serial.println("Initialized Successfully!");
  else
    Serial.println("**Error Initializing**");

//  CAN0.init_Mask(0,0x7F00000);                // Init first mask...
  CAN0.init_Filt(0,0x7DF0000);                // Init first filter...
  CAN0.init_Filt(1,0x7E10000);                // Init second filter...
//  CAN0.init_Mask(1,0x7F00000);                // Init second mask... 
  CAN0.init_Filt(2,0x7DF0000);                // Init third filter...
  CAN0.init_Filt(3,0x7E10000);                // Init fouth filter...
  CAN0.init_Filt(4,0x7DF0000);                // Init fifth filter...
  CAN0.init_Filt(5,0x7E10000);                // Init sixth filter...  
  
  CAN0.setMode(MCP_NORMAL);   // Set operation mode to normal so the MCP2515 sends acks to received data.

}

void loop()
{
  if (Serial.available() > 1) {
          // read the incoming byte:
      Bmode = Serial.read();
      Bpid = Serial.read();
      req(Bmode,Bpid);
  }
}

void req(unsigned char mode, unsigned char pid){
  unsigned char txData[] = {0x02,mode,pid,0x0,0x0,0x0,0x0,0x0};

  CAN0.sendMsgBuf(0x7DF, 8, txData);
  delay(500);
  read1();
}

void read1(){
    if(!digitalRead(2)){
        for(byte i=0;i<20;++i){       //get 20 times unless reply arrived from the ECU 
        CAN0.readMsgBuf(&rxId, &len, rxBuf);     // Get CAN data - 03 41 0C 3D 0F
         if(rxId == 0x7E8){
           Serial.write(rxBuf,8);
           Serial.flush();
            break;
        }

      }
    }   
} 
