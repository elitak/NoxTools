#ifndef THING_LIB
#define THING_LIB


#include "ClassDefs.h"
using namespace std;

  struct Property
   {
	   unsigned int ValueLen;
	   BYTE Value[512];
   };
///////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////
struct GAMI
{
  BYTE Name[50];
  BYTE aniName[50];
  BYTE nameLen;
  unsigned int ImageCode;//[70];
  LIST<unsigned int> Images;
  //int numImages;
  //Image_List List;

   GAMI()
   {
      memset(this,0x00,sizeof(*this));
   }
};

///////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////
class LOOP
{
	private:
    public:
	unsigned char Name[50];
    LIST<unsigned int> Images;
	//Image_List List;
};

///////////////////////////////////////////////////////////
   class TATS
   {	
	 public:
           unsigned int SubCat;
           BYTE loopName[50];
		   LIST<unsigned int> Images;
           BYTE nullString[50];
     TATS()
     {
        SubCat = 0;
     }
	 ~TATS()
	 {
		 Images.Clear();
	 }
   };

///////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////

struct GNHT // Object Class
{
   BYTE obType;
   bool typeFound;

   char Name[50];
   BYTE nameLen;
   //char HeaderNames[5][20];
   int Num_Headers;
   int Header_Nums[5];
   unsigned int ImageCode;
   //Image_List List;
   //LIST<unsigned int> Images;
 
   //int numStrings;
   //char propStrings[25][255];

   LIST<Property> Properties;

 
   //IMAGE AND ANIMATION STRUCTS
   unsigned int MenuIcon;
   unsigned int PrettyImage;

   char aniName[255];

   //PLAYER/MONSTER DRAW FUNCS
   
   //TATS Stat[70];
   //int numStats;
   LIST<TATS> Stats;
   LIST<TATS> PlayerStats;
   //TATS *PlayerStat;
   //int numPlayerStats;
   LIST<Property> Subcats;
   //CString *SubCats;
   //int numSubCats;
   ///////////////////////////   
   GNHT()
   {   
      memset(this,0x00,sizeof(*this));	   
   }
   ~GNHT()
   {
   }

};


//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
struct LLAW
{
       unsigned int Unknown; //In everything, ID maybe?
       BYTE nameLen;
       BYTE Name[50];
       BYTE Unknowns[14];//14 of them; may not be BYTES
       //long numObjects;
	   LIST<Property> Objects;

       BYTE Sound_Open_Len;
       BYTE SoundOpen[50]; 
       BYTE Sound_Close_Len;
       BYTE SoundClose[50]; 
       BYTE Sound_Destroy_Len;
       BYTE SoundDestroy[50]; 
       //int numImages;
       unsigned int ImageCode;
	   LIST<unsigned int> Images;

   LLAW()
   {
      memset(this,0x00,sizeof(*this));	
   }

};




//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
struct ROLF
{
      int Unknown; //In everything, maybe be type ID
      BYTE nameLen;
      char Name[50];
      short Unknown2; //no idea
      BYTE Unknowns[14]; //14 unknown bytes
      short one; //should be 0x01
      //int ImageCode;
	  LIST<unsigned int> Images;
	  //Image_List List;

   ROLF()
   {
      memset(this,0x00,sizeof(*this));
   }
};


//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
struct GAMISTRUCT
{
public:
   LIST<GAMI> Images;
   GAMISTRUCT()
   {}
   //{delete [] Tiles;} //ERROR PRONE?
};

//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
struct ROLFSTRUCT
{
public:
  LIST<ROLF> Tiles;

  ROLFSTRUCT()
  {}
//{delete [] Tiles;} //ERROR PRONE?
};


//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
struct LLAWSTRUCT
{
public:
	LIST<LLAW> Walls;

    LLAWSTRUCT()
	{}
};

//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
struct GNHTSTRUCT
{
	public:
		LIST<GNHT> Objects;

//~LLAWSTRUCT()
//{delete [] Walls;} //ERROR PRONE?
		~GNHTSTRUCT()
		{
		}
};


//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
struct ThingDB
{
   LLAWSTRUCT Wall;
   ROLFSTRUCT Tile;
   GNHTSTRUCT Object;
   GAMISTRUCT Image;

~ThingDB()
{}
};
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////
class ThingBin
{
public:
         char Type[5];
         ThingDB Thing;
		 bool Is_Loaded;

         fstream *file;
         fstream out;

         bool Load_Thingdb(char FileName[]);
         bool Load_Tiles(unsigned char *buff,long &count,ROLFSTRUCT & Data);
         bool Load_Walls(unsigned char *buff,long &count,LLAWSTRUCT & Data);
         bool Load_Images(unsigned char *buff,long &count,GAMISTRUCT & Data);
         bool Load_Objects(unsigned char *buff,long &count,long filelen,GNHTSTRUCT & Data);

/*int Get_Object_Loc(char ObName[100])
{
  for(int i=0; i<Thing.Object.numObjects; i++)
  {
    if(!_stricmp(ObName,Thing.Object.Objects[i].Name))
	{return(i+1);}
  }
  return(0);
}*/

   ThingBin()
   {
      Is_Loaded = false;
   }
   ~ThingBin()
   {
   }


      private:
              bool Load_Tile(long &count,unsigned char *buff, ROLF & Data); 
              bool Load_Wall(long &count,unsigned char *buff, LLAW & Data); 
              bool Load_Image(long &count,unsigned char *buff, GAMI & Data); 
              bool Load_Audio(long &count,unsigned char *buff); 
              bool Load_Object(long &count,unsigned char *buff,long filelen, GNHT & Data); 

			  int Parse_String(char str[],int strLen);
};







#endif