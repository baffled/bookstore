!INCLUDE
COMMON /U2_BOOKS/ U2_FILES(20),U2_INIT

EQU F.BOOKS         TO U2_FILES(1)
EQU F.CLIENTS       TO U2_FILES(2)
EQU F.AUTHORS       TO U2_FILES(3)
EQU F.ORDERS        TO U2_FILES(4)
EQU F.PURCHASES     TO U2_FILES(5)
EQU F.PUBLISHERS    TO U2_FILES(6)
EQU F.SUPPLIERS     TO U2_FILES(7)
EQU F.PAYMENTS      TO U2_FILES(8)
EQU F.CLIENT.ORDERS TO U2_FILES(9)
EQU F.PROMOTIONS    TO U2_FILES(10)
EQU F.SHIPPING      TO U2_FILES(11)
EQU F.PERSONNEL     TO U2_FILES(12)
EQU F.SALESTAX      TO U2_FILES(13)
EQU F.TARGETS       TO U2_FILES(14)
EQU F.PARAMS        TO U2_FILES(15)

! Self initializing common
If U2_INIT = 0 Then
   U2_INIT = @True
   Call u2_init
End



