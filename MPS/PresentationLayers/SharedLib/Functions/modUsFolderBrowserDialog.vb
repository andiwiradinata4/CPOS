Module modUsFolderBrowserDialog
    Public Class usFolderBrowserDialog
        Public Shared Function Show(ByRef bolSuccess As Boolean) As String
            Dim ofd As New FolderBrowserDialog
            Dim strPath As String = ""
            Try
                With ofd
                    If .ShowDialog() = Windows.Forms.DialogResult.OK Then
                        strPath = .SelectedPath
                        bolSuccess = True
                    End If
                End With
            Catch ex As Exception
                UI.usForm.frmMessageBox(ex.Message)
                bolSuccess = False
                strPath = ""
            End Try
            Return strPath
        End Function
    End Class

End Module
