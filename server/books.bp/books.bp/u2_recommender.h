!INCLUDE
* ---------------------------------------------------------------------------
*  @@Name        : u2_recommender.h
*  @@Description : Constants for the book recommendations
*  @@Version     : 1.0
* ---------------------------------------------------------------------------
*  Brief Description
*  -----------------
*  @@INFO {
*  }
* ---------------------------------------------------------------------------
*  Warnings
*  --------
*
* ---------------------------------------------------------------------------
*  Modification History
*  --------------------
*  @@Log
* ---------------------------------------------------------------------------
* Keywords
* --------
*
* --------------------------------------------------------------------------
* To Do List
* ----------
*
* --------------------------------------------------------------------------
COMMON /MODEL/ hClientsToBooks, hBooksToClients
EQU RECOMMEND_ACTION.BUILD        TO 1
EQU RECOMMEND_ACTION.ORDER        TO 2
EQU RECOMMEND_ACTION.RECOMMEND    TO 3
EQU RECOMMEND_ACTION.REPORT       TO 98
EQU RECOMMEND_ACTION.CLEAR        TO 99
* Apply weightings
EQU RECOMMEND_WEIGHT.AUTHOR       TO 3
EQU RECOMMEND_WEIGHT.GENRE        TO 2

