Imports System.IO

Namespace SharedLib
    Public Class DirectoryUtility

        Public Shared Function CopyFile(ByVal strSourcePath As String, ByVal strDestinationPath As String, ByVal strFileName As String, Optional ByVal bolOverwrite As Boolean = True) As String
            Dim strReturn As String = ""
            Dim strFileInfo As New IO.FileInfo(strSourcePath)
            Dim strExtension As String = strFileInfo.Extension
            strReturn = strDestinationPath & "\" & strFileName & strExtension
            If IO.File.Exists(strReturn) Then
                FlushMemory()
            End If
            IO.File.Copy(strSourcePath, strReturn, bolOverwrite)
            Return strReturn
        End Function

        Public Shared Function MoveFile(ByVal strSourcePath As String, ByVal strDestinationPath As String, ByVal strFileName As String) As String
            Dim strFileInfo As New IO.FileInfo(strSourcePath)
            Dim strExtension As String = strFileInfo.Extension
            strDestinationPath = strDestinationPath & "\" & strFileName & strExtension
            If IO.File.Exists(strDestinationPath) Then
                DeleteFile(strDestinationPath)
            End If
            IO.File.Move(strSourcePath, strDestinationPath)
            Return strDestinationPath
        End Function

        Public Shared Sub DeleteFile(ByVal strPath As String)
            My.Computer.FileSystem.DeleteFile(strPath)
        End Sub

        Declare Function SetProcessWorkingSetSize Lib "kernel32.dll" (ByVal process As IntPtr, ByVal minimumWorkingSetSize As Integer, ByVal maximumWorkingSetSize As Integer) As Integer
        Public Shared Sub FlushMemory()
            Try
                GC.Collect()
                GC.WaitForPendingFinalizers()
                If (Environment.OSVersion.Platform = PlatformID.Win32NT) Then
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, -1, -1)
                    Dim myProcesses As Process() = Process.GetProcessesByName("ApplicationName")
                    For Each myProcess As Process In myProcesses
                        SetProcessWorkingSetSize(myProcess.Handle, -1, -1)
                    Next
                End If
            Catch ex As Exception

            End Try
        End Sub

        Public Shared Function ConvertImagetoByte(ByVal strPath As String) As Byte()
            Dim fs As New FileStream(strPath, FileMode.Open, FileAccess.Read)
            Dim bytReturn As Byte() = New Byte(fs.Length - 1) {}
            fs.Read(bytReturn, 0, System.Convert.ToInt32(fs.Length))
            fs.Dispose()
            Return bytReturn
        End Function

    End Class
End Namespace

