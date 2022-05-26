Imports System.Xml

Public Class usXML
    Dim Doc As New XmlDocument
    Dim FileName As String
    Dim doesExist As Boolean

    Public Sub New(ByVal aFileName As String)
        FileName = aFileName
        Try
            Doc.Load(aFileName)
            doesExist = True
        Catch ex As Exception
            If Err.Number = 53 Then
                Doc.LoadXml(("<configuration>" & "</configuration>"))
                Doc.Save(aFileName)
            End If
        End Try
    End Sub

    Public Function GetConfigInfo(ByVal aSection As String, ByVal aKey As String, ByVal aDefaultValue As String) As Collection
        If doesExist = False Then
            Return New Collection
        End If
        If aSection = "" Then
            Return getchildren("")
        ElseIf aKey = "" Then
            Return getchildren(aSection)
        Else
            Dim col As New Collection
            col.Add(getKeyValue(aSection, aKey, aDefaultValue))
            Return col
        End If
    End Function

    Public Function WriteConfigInfo(ByVal aSection As String, ByVal aKey As String, ByVal aValue As String) As Boolean
        Dim node1 As XmlNode
        Dim node2 As XmlNode
        If aKey = "" Then
            node1 = (Doc.DocumentElement).SelectSingleNode("/configuration/" & aSection)
            If node1 Is Nothing Then Return True
            node1.RemoveAll()
            node2 = (Doc.DocumentElement).SelectSingleNode("configuration")
            node2.RemoveChild(node1)
        ElseIf aValue = "" Then
            node1 = (Doc.DocumentElement).SelectSingleNode("/configuration/" & aSection)
            If node1 Is Nothing Then Return True
            node2 = (Doc.DocumentElement).SelectSingleNode("/configuration/" & aSection & "/" & aKey)
            If node2 Is Nothing Then Return True
            If node1.RemoveChild(node2) Is Nothing Then Return False
        Else
            node1 = (Doc.DocumentElement).SelectSingleNode("/configuration/" & aSection & "/" & aKey)
            If node1 Is Nothing Then
                node2 = (Doc.DocumentElement).SelectSingleNode("/configuration/" & aSection)
                If node2 Is Nothing Then
                    Dim e As Xml.XmlElement = Doc.CreateElement(aSection)
                    node2 = Doc.DocumentElement.AppendChild(e)
                    If node2 Is Nothing Then Return False
                    e = Doc.CreateElement(aKey)
                    e.InnerText = aValue
                    If (node2.AppendChild(e)) Is Nothing Then Return False
                Else
                    Dim e As Xml.XmlElement = Doc.CreateElement(aKey)
                    e.InnerText = aValue
                    node2.AppendChild(e)
                End If
            Else
                node1.InnerText = aValue
            End If
        End If
        Doc.Save(FileName)
        Return True
    End Function

    Private Function getKeyValue(ByVal aSection As String, ByVal aKey As String, ByVal aDefaultValue As String) As String
        Dim node As XmlNode
        node = (Doc.DocumentElement).SelectSingleNode("/configuration/" & aSection & "/" & aKey)
        If node Is Nothing Then Return aDefaultValue
        Return node.InnerText
    End Function

    Private Function getchildren(ByVal aNodeName As String) As Collection
        Dim col As New Collection
        Dim node As XmlNode = Doc.DocumentElement
        Try
            If aNodeName = "" Then
                node = Doc.DocumentElement
            Else
                node = Doc.DocumentElement.SelectSingleNode(aNodeName)
            End If
        Catch
        End Try
        If node Is Nothing Then Return col
        If node.HasChildNodes = False Then Return col
        Dim nodeList As XmlNodeList = node.ChildNodes
        Dim i As Integer
        For i = 0 To nodeList.Count - 1
            col.Add(nodeList.Item(i).Name)
        Next
        Return col
    End Function

End Class
