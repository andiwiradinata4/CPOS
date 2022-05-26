Imports System.IO
Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports System.Text

Public Class Encryption

    Public Shared Function PasswordHash() As String
        Return "H4SH-P4SSW0RD"
    End Function

    Public Shared Function SaltKey() As String
        Return "S@LT-KEY"
    End Function

    Public Shared Function VIKey() As String
        Return "!@#KueMantul#0001"
    End Function

    Public Shared Function Encrypt(ByVal strValue As String) As String
        Dim bytValues() As Byte = Encoding.UTF8.GetBytes(strValue)
        Dim bytKeys() As Byte = New Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8)
        Dim symetricKey = New RijndaelManaged()
        With symetricKey
            .Mode = CipherMode.CBC
            .Padding = PaddingMode.Zeros
        End With
        Dim encryptor = symetricKey.CreateEncryptor(bytKeys, Encoding.ASCII.GetBytes(VIKey))
        Dim cipherTextBytes() As Byte

        Using ms As New MemoryStream()
            Using cryptoStream As New CryptoStream(ms, encryptor, CryptoStreamMode.Write)
                cryptoStream.Write(bytValues, 0, bytValues.Length)
                cryptoStream.FlushFinalBlock()
                cipherTextBytes = ms.ToArray
                cryptoStream.Close()
            End Using
            ms.Close()
        End Using
        Return Convert.ToBase64String(cipherTextBytes)
    End Function

    Public Shared Function Decrypt(ByVal strValue As String) As String
        Dim cipherTextBytes() As Byte = Convert.FromBase64String(strValue)
        Dim bytKeys() As Byte = New Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8)
        Dim symetricKey = New RijndaelManaged()
        With symetricKey
            .Mode = CipherMode.CBC
            .Padding = PaddingMode.Zeros
        End With
        Dim decryptor = symetricKey.CreateDecryptor(bytKeys, Encoding.ASCII.GetBytes(VIKey))
        Dim ms As New MemoryStream(cipherTextBytes)
        Dim cryptoStream As New CryptoStream(ms, decryptor, CryptoStreamMode.Read)
        Dim bytValues(cipherTextBytes.Length) As Byte
        Dim decryptedByteCount = cryptoStream.Read(bytValues, 0, bytValues.Length)
        cryptoStream.Close()
        ms.Close()
        Return Encoding.UTF8.GetString(bytValues, 0, decryptedByteCount).TrimEnd("\0".ToCharArray)
    End Function

End Class
