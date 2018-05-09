      PROGRAM BUILD
*------------------------------------------------------------------------------
*  @@Name        : BUILD
*  @@Description : build the demonstration database from the repository
*  @@Author      : Brian Leach
*  @@Project     : TOOLS
*--------------------------------------------------------------------------
*  Notes
*  -----
*  @@INFO{
*    The demonstration database is held in a GIT repository, but this cannot
*    handle UniVerse data files, dictionaries or indices, and does not manage
*    the object code.
*
*    This routine, called from build.bat, creates and refreshes the account.
*  }
*--------------------------------------------------------------------------
*  Modification History
*  --------------------
*
*  Date        Who  Version     Description
*  ----        ---  -------     -----------
*--------------------------------------------------------------------------
*  Version Stamp
*  -------------
VERDATA=''
VERDATA := 'Version=001000000;'
VERDATA := 'VerBeta=;'
VERDATA := 'VerDate=16570;'
VERDATA := 'VerProd=BLUTILS;'
VERDATA := 'VerTM=;'
VERDATA := 'VerCopy=2018 Brian Leach Consulting Limited;'
VERDATA := 'VerCo=Brian Leach Consulting Limited;'
VERDATA := 'VerDesc=Build database from repository;'
VERDATA := 'CatName=BUILD;'
VERDATA := 'VerModule=GENERAL;'
VERDATA := 'VerModVer=001000000;'
VERDATA := 'VerHist=;'
*--------------------------------------------------------------------------
! The MIT License
! ===============
! Copyright (c) 2018 Brian Leach
!
! Permission is hereby granted, free of charge, to any person obtaining a copy
! of this software and associated documentation files (the "Software"), to deal
! in the Software without restriction, including without limitation the rights
! to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
! copies of the Software, and to permit persons to whom the Software is
! furnished to do so, subject to the following conditions:
!
! The above copyright notice and this permission notice shall be included in
! all copies or substantial portions of the Software.
!---------------------------------------------------------------------------
! DISCLAIMER:
!
! The SOFTWARE is being delivered to you "AS IS" and BRIAN LEACH makes no
! warranty as to its use or performance. BRIAN LEACH AND ITS SUPPLIERS DO NOT
! WARRANT THE PERFORMANCE OR RESULTS YOU MAY OBTAIN BY USING THE SOFTWARE OR
! DOCUMENTATION. BRIAN LEACH AND ITS SUPPLIERS MAKE NO WARRANTIES, EXPRESS OR
! IMPLIED, AS TO NONINFRINGEMENT OF THIRD PARTY RIGHTS, MERCHANTABILITY, OR
! FITNESS FOR ANY PARTICULAR PURPOSE. IN NO EVENT WILL BRIAN LEACH OR ITS
! SUPPLIERS BE LIABLE TO YOU FOR ANY CONSEQUENTIAL, INCIDENTAL OR SPECIAL DAMAGES,
! INCLUDING ANY LOST PROFITS OR LOST SAVINGS, EVEN IF A BRIAN LEACH REPRESENTATIVE
! HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES, OR FOR ANY CLAIM BY ANY
! THIRD PARTY.
!---------------------------------------------------------------------------	  
* refresh the bookstore account
      Open 'VOC' To F.VOC Else
         Crt 'CAnnot open VOC'
         STOP
      End
      RepoPath = "..\..\repository\server"
      Fatal = @False
      GoSub Init
      GoSub Main
      STOP

*--------------------------------------------------------------------------
* Initialize
*--------------------------------------------------------------------------
Init:
      FileList = 'books.bp'
      FileList<-1> = 'books.inc'
      FileList<-1> = 'books.pa'
      FileList<-1> = 'books.pysrc'
      FileList<-1> = 'books.scripts'
      FileList<-1> = 'books.source'
      FileList<-1> = 'books.tables'
      FileList<-1> = 'test.bp'
      FileList<-1> = 'test.source'
      FileList<-1> = 'images'
	
	FileList<-1> = 'TESTS'
	FileList<-1> = 'TEST_BACKOUT'
	FileList<-1> = 'TEST_BATCH'
	FileList<-1> = 'TEST_DBG'
	FileList<-1> = 'TEST_INC'
	FileList<-1> = 'TEST_LOCAL'
	FileList<-1> = 'TEST_MOCKS'
	FileList<-1> = 'TEST_OUT'
	FileList<-1> = 'TEST_PROFILE'
	FileList<-1> = 'TEST_RESULTS'
	FileList<-1> = 'TEST_RUN'
	FileList<-1> = 'TEST_SCRIPTS'
	FileList<-1> = 'TEST_STATE'
	
      Return

*--------------------------------------------------------------------------
* Main Routine
*--------------------------------------------------------------------------	  
Main:
* first we compile and catalog our routines
      Perform 'BASIC blutil.bp *'
      Perform 'CATALOG blutil.bp *'

	  * First point to special files (needed for includes)
      GoSub Specials
	  
* Now create all the tables from the definitions
      GoSub BuildTables

* Now populate them from the repository
      GoSub Populate

      Return
*--------------------------------------------------------------------------
* build tables by calling MAKETABLE for each of the book tables
*--------------------------------------------------------------------------	  
BuildTables:

      FileName = 'books.tables'
      GoSub CreatePointer
      If Fatal Then Return
      Perform 'MAKETABLE books.tables.safe * /NOINCLUDES'
      F.TABLES = F.SAFE
      Return

*--------------------------------------------------------------------------
* Create Pointer to a file in the repository
*--------------------------------------------------------------------------	  
CreatePointer:
      VOCRec = 'F':@FM:RepoPath:'\':FileName:@FM:'D_VOC'
      Write VOCRec On F.VOC, FileName:'.safe'
      Open FileName:'.safe' To F.SAFE Else
         Fatal = @TRUE
         Crt 'Error creating pointer to ':FileName
         Return
      End
      Return
*--------------------------------------------------------------------------
* Populate the local file from the repository data
*--------------------------------------------------------------------------
Populate:
      Execute 'SSELECT books.tables.safe' RTNLIST SLIST
      Fin = @False
      Loop
         ReadNext FileName From SLIST Else Fin = @TRUE
      Until Fin Do
         If Not(FileName = 'DEFAULT') Then
		    Fatal = @FALSE
            GoSub CreatePointer
			If Not(Fatal) Then
               Cmd = 'COPYI FROM ':FileName:'.safe TO ':FileName:' ALL OVERWRITING'
			End
            Perform Cmd
         End
      Repeat
      Return

*--------------------------------------------------------------------------
* Update the other (non-data) files
*--------------------------------------------------------------------------
Specials:
      NoFiles = DCount(FileList,@FM)
      For ThisFile = 1 To NoFiles
         FileName = FileList<ThisFile>
         VOCRec = 'F':@FM:RepoPath:'\':FileName:@FM:'D_VOC'
         If FileName['.',2,1] = 'bp' Then
            VOCRec<4> = 'M'
            VOCRec<7> = FileName:@VM:'unidata'
            VOCRec<8> = FileName:@VM:'unidata'
         End
         Write VOCRec On F.VOC, FileList<ThisFile>
      Next
	  
* Now compile and catalog as required
      For ThisFile = 1 To NoFiles
         FileName = FileList<ThisFile>
         If FileName['.',2,1] = 'bp' Then
            Cmd = 'CREATE.FILE ':FileName:'.O 1,1 1,1,19'
            Perform Cmd
            Cmd = 'BASIC ':FileName:' *'
            Perform Cmd
            Cmd = 'CATALOG ':FileName:' *'
            Perform Cmd
         End
      Next
      Return
