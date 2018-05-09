 ' Build script for bookstore
 ' This (re)creates or refreshes the bookstore UniVerse workspace
 
Const DEF_UV_PATH = "c:\u2\uv"
Const ACC_HOME = "..\universe"

Dim UV
Dim FSO
Dim WSH

Dim HostName
Dim UserName
Dim AccountPath
Dim AccountHome
Dim Password   

Dim AllDone
Dim Home

Dim UV_PATH


' Initialize helper objects

Set FSO = CreateObject("Scripting.FileSystemObject")
Set WSH = CreateObject("WScript.Shell")

' Ensure this is running CSCRIPT not WSCRIPT

If UCase( Right( WScript.FullName, 12 ) ) <> "\CSCRIPT.EXE" Then
   Crt "Please run me using CSCRIPT not WSCRIPT"
Else
   AllDone = False
   UV_PATH = DEF_UV_PATH
   Main
End If

'----------------------------------------------------------------------
' Main
'----------------------------------------------------------------------
Sub Main
   Home = WSH.CurrentDirectory
   Crt "Home = " & Home
   If ShowScreen Then
      If CreateAccountHome Then
         If CreateAccount Then
            If RefreshAccounts Then
			   StartUniverse
			End If
         End If
      End If
   End If
End Sub


'----------------------------------------------------------------------
' CreateAccountHome
'----------------------------------------------------------------------
Function CreateAccountHome
   If FSO.FolderExists(ACC_HOME) = False Then
      If CreateDirectory(ACC_HOME) = False Then
         CreateAccountHome = False
         Exit Function
      End If
   End If
   If ChangeDirectory(ACC_HOME) = False Then
      CreateAccountHome = False
      Exit Function
   End If
    
   AccountHome = WSH.CurrentDirectory
	
   Crt "Account Home= " & AccountHome
	
   CreateAccountHome = True
End Function	

'----------------------------------------------------------------------
' CreateAccount - create the account structure if it does not exist
'----------------------------------------------------------------------
Function CreateAccount

    Dim Accts
	Dim I
	Dim ThisAccount
	Dim ThisVOC
	
	ThisAccount = MakePath(AccountHome, "mv_books")
	Crt "ThisAccount = " & ThisAccount
    If FSO.FolderExists(ThisAccount) = False Then
       If CreateDirectory(ThisAccount) = False Then
          CreateAccount = False
           Exit Function
        End If
    End If
      
	If ChangeDirectory(ThisAccount) = False then
	   CreateAccount = False
	   Exit Function
 	End if
	   
	ThisVOC = MakePath(ThisAccount, "VOC")
    If FSO.FileExists(ThisVOC) = False Then		   
	   WinExec MakePath(UV_PATH,"bin\mkaccount PICK")
	Else
	   ' Make sure subsequent commands don't hit account updates
	   WinExec MakePath(UV_PATH,"bin\uv UPDATE.ACCOUNT")		
	End if         	   
	CreateAccount = True
End Function

'----------------------------------------------------------------------
' RefreshAccounts - Call a routine to refresh the account content
'----------------------------------------------------------------------
Function RefreshAccounts
   Dim Result
   
   Dim Cmd
   
   ' create file pointer to blutil.bp
   Cmd = MakePath(UV_PATH,"bin\UVwrite VOC blutil.bp F ..\..\repository\server\blutil.bp")
   WinExec Cmd
   
   ' create a local blutil.bp.O file
   Cmd = MakePath(UV_PATH,"bin\uv CREATE.FILE blutil.bp.O 1,1 1,1,19")
   WinExec Cmd
   
   ' now catalog and run the build program
   Cmd = MakePath(UV_PATH,"bin\uv BASIC blutil.bp BUILD")
   WinExec Cmd
   
   Cmd = MakePath(UV_PATH,"bin\uv RUN blutil.bp BUILD")
   WinExec Cmd
   
   RefreshAccounts = True
End Function

'---------------------------------------------------------------
' ShowScreen
'---------------------------------------------------------------
Function ShowScreen
    
    Dim Refresh, Done
    Refresh = true
    Done = False
    
    Do
      if Refresh then
	    Refresh = False
    	WScript.Echo "Build Bookstore Account"
    	WScript.Echo "-----------------------"
    	WScript.Echo "Options:"
    	WScript.Echo " 1. UniVerse Home   : " & UV_PATH
      end if
    
      WScript.StdOut.Write "Option Continue or Quit : "
      ans = Input
      if ans = "Q" then
         ShowScreen = False
         Done = true
      End If
      
      If ans = "C" then
         ShowScreen = True
         Done = true
      End if
      
      If ans = "1" then
         WScript.StdOut.Write "Specify UniVerse Path : "
         temp = Input
         if temp <> "" then
            UV_PATH = temp
         end if         
         Refresh = True
      End if
            
   Loop Until Done
    
End Function

'----------------------------------------------------------------------
' StartUniverse
'----------------------------------------------------------------------
Sub StartUniverse
   Dim Cmd
   Crt "---------------------------------------------------------------------"
   Crt "Starting the demonstration bookstore."
   Crt "Type OFF to quit when you have finished"
   Crt "---------------------------------------------------------------------"
   
   ' create file pointer to blutil.bp
   Cmd = MakePath(UV_PATH,"bin\uv")
   WinExec Cmd
   Crt "---------------------------------------------------------------------"
   Crt "Thank you for using the demonstration bookstore."
   Crt "To return to this bookstore, first change directory to :"
   Crt MakePath(AccountHome, "mv_books")
   Crt "Then type the command:"
   Crt Cmd
   Crt "---------------------------------------------------------------------"
   PressAnyKey
End Sub

'----------------------------------------------------------------------
'=======================| Helper Routines  |===========================
'----------------------------------------------------------------------
'----------------------------------------------------------------------
' ChangeDirectory
'----------------------------------------------------------------------
Function ChangeDirectory(Path)
  On Error Resume Next
  Err = False
  WSH.CurrentDirectory = Path
  If Err = False then
    ChangeDirectory = True
  Else
    Fatal "Could not change directory to " & Path
    ChangeDirectory = False
  End if
End Function

'----------------------------------------------------------------------
' Continue
'----------------------------------------------------------------------
Function Continue
  Dim Ok
  Ok = Inputbox("Continue","Continue","Yes")
  If UCase(Ok) = "YES" then
     Continue = True
  Else
    Continue = False
  End if
End function

'----------------------------------------------------------------------
' CopyDirectory
'----------------------------------------------------------------------
Function CopyDirectory(Source, Target)
  On Error Resume Next
  Err = False
  FSO.CopyFolder Source, Target, True
  If Err = False Then
     CopyDirectory = True
  Else
     Fatal "Could not copy folder " & Source & " to " & Target
     CopyDirectory = False
  End If
End function

'----------------------------------------------------------------------
' CopyFile
'----------------------------------------------------------------------
Function CopyFile(Source, Target)
  On Error Resume Next
  Err = False
  FSO.CopyFile Source, Target, True
  If Err = False Then
     CopyFile = True
  Else
     Fatal "Could not copy file " & Source & " to " & Target
     CopyFile = False
  End If
End function

'----------------------------------------------------------------------
' CreateDirectory
'----------------------------------------------------------------------
Function CreateDirectory(Path)
  CreateDirectory = True
  On Error resume next
  Err = False
  If FSO.FolderExits(Path) = False Then
     Err = False
     FSO.CreateFolder(Path)
  End If
  If Err <> False Then
     Fatal "Could not create folder " & Path
     CreateDirectory = False
  End if
End Function 

'----------------------------------------------------------------------
' Crt
'----------------------------------------------------------------------
Sub Crt(Text)
  WScript.Echo Text
End Sub

'----------------------------------------------------------------------
' DeleteFile
'----------------------------------------------------------------------
Function DeleteFile(Path)
  On Error Resume Next
  If FSO.FileExists(Path) = False Then
     DeleteFile = True
	 Exit Function
  End If
  Err = false
  FSO.DeleteFile(Path)
  If Err = False Then
     DeleteFile = True
	 Exit Function
  End if
  Fatal "Could not delete file " & Path
  DeleteFile = False
End Function

'----------------------------------------------------------------------
' DQuote
'----------------------------------------------------------------------
Function DQuote(Text)
   DQuote = """" & Text & """"
End Function

'----------------------------------------------------------------------
' Error
'----------------------------------------------------------------------
Sub CrtError(Text)
  WScript.StdOut.Write "ERROR : " & Text & " :"
  Input
End Sub

'----------------------------------------------------------------------
' Fatal
'----------------------------------------------------------------------
Sub Fatal(Text)
  WScript.Echo Text
  ' TBD set return code and stop
End Sub

'----------------------------------------------------------------------
' Input
'----------------------------------------------------------------------
Function Input
  Input = WScript.StdIn.ReadLine
End Function

'----------------------------------------------------------------------
' MakePath
'----------------------------------------------------------------------
Function MakePath(Path, FileName)
  Dim T

  If Right(Path,1) = "\" Then  
   T = Path & FileName
  Else
   T = Path & "\" & FileName
  End If
  MakePath = T
End Function 

'----------------------------------------------------------------------
' PressAnyKey
'----------------------------------------------------------------------	
Sub PressAnyKey
   WScript.StdOut.Write("Press any key to continue")
   Input
End Sub
'----------------------------------------------------------------------
' ReadFile
'----------------------------------------------------------------------	
Function ReadFile(Path)

  On Error Resume Next
  Dim MyFile
  
  Set MyFile = FSO.OpenTextFile(Path, 1)
  If MyFile.AtEndOfStream Then
     ReadFile = ""
  Else
     ReadFile = MyFile.ReadAll
  End If
  MyFile.Close 
End Function

'----------------------------------------------------------------------
' WinExec
'----------------------------------------------------------------------	
Sub WinExec( Cmd )
  Crt "Running " & Cmd
  WSH.Run Cmd, 1, True
End Sub

'----------------------------------------------------------------------
' WriteFile
'----------------------------------------------------------------------	
Function WriteFile(path, content)
   On Error Resume Next
   Dim MyFile
   
   If FSO.FileExists(path) Then
      FSO.DeleteFile(path)
   End If

   Set MyFile = FSO.CreateTextFile(path, true)
   MyFile.write(content)
   MyFile.close
   
   WriteFile = (Err = False)
End Function

'----------------------------------------------------------------------
' YesNo
'----------------------------------------------------------------------	
Function YesNo(AnArg)
   if AnArg then
      YesNo = "Yes"
   Else
      YesNo = "No"
   End if
 End Function
 
