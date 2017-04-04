﻿Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Text
Imports GroupDocs.Metadata.Examples.VBasic.Utilities
Imports GroupDocs.Metadata.Exceptions
Imports GroupDocs.Metadata.Formats
Imports GroupDocs.Metadata.Formats.Document
Imports GroupDocs.Metadata.Formats.Project
Imports GroupDocs.Metadata.Tools
Imports GroupDocs.Metadata.Xmp
Imports GroupDocs.Metadata.Xmp.Schemes
Imports Mtdta = GroupDocs.Metadata




Namespace GroupDocs.Metadata.Examples.VBasic
    Public NotInheritable Class Documents
        Private Sub New()
        End Sub
        Public NotInheritable Class Doc
            Private Sub New()
            End Sub
            ' initialize file path
            'ExStart:SourceDocFilePath
            Private Const filePath As String = "Documents/Doc/sample1.doc"
            'ExEnd:SourceDocFilePath
#Region "working with built-in document properties"

            ''' <summary>
            ''' Gets builtin document properties from Doc file 
            ''' </summary> 
            Public Shared Sub GetDocumentProperties()
                Try
                    'ExStart:GetBuiltinDocumentPropertiesDocFormat
                    ' initialize DocFormat
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    ' initialize metadata
                    Dim docMetadata As DocMetadata = docFormat.DocumentProperties

                    ' get properties
                    Console.WriteLine("Built-in Properties: ")
                    For Each [property] As KeyValuePair(Of String, PropertyValue) In docMetadata
                        ' check if built-in property
                        If docMetadata.IsBuiltIn([property].Key) Then
                            Console.WriteLine("{0} : {1}", [property].Key, [property].Value)
                        End If
                        'ExEnd:GetBuiltinDocumentPropertiesDocFormat
                    Next
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub


            ''' <summary>
            ''' Reads all metadata keys of the Word document
            ''' </summary> 
            ''' <param name="directoryPath">Path to the files</param>
            Public Shared Sub ReadMetadataUsingKeys(directoryPath As String)
                Try
                    'ExStart:ReadMetadataUsingKeys
                    'Get all Word documents inside directory
                    Dim files As String() = Directory.GetFiles(Common.MapSourceFilePath(directoryPath), "*.doc")

                    For Each path__1 As String In files
                        Console.WriteLine("Document: {0}", Path.GetFileName(path__1))

                        ' open Word document
                        Using doc As New DocFormat(path__1)
                            ' get metadata
                            Dim metadata As Mtdta.Metadata = doc.DocumentProperties

                            ' print all metadata keys presented in DocumentProperties
                            For Each key As String In metadata.Keys
                                Console.WriteLine(key)
                            Next
                        End Using
                    Next
                    'ExEnd:ReadMetadataUsingKeys
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            End Sub



            ''' <summary>
            ''' Updates document properties of Doc file and creates output file
            ''' </summary> 
            Public Shared Sub UpdateDocumentProperties()
                Try
                    'ExStart:UpdateBuiltinDocumentPropertiesDocFormat
                    ' initialize DocFormat
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    ' initialize DocMetadata
                    Dim docMetadata As DocMetadata = docFormat.DocumentProperties

                    'update document property...
                    docMetadata.Author = "Usman"
                    docMetadata.Company = "Aspose"
                    docMetadata.Manager = "Usman Aziz"

                    'save output file...
                    docFormat.Save(Common.MapDestinationFilePath(filePath))

                    'ExEnd:UpdateBuiltinDocumentPropertiesDocFormat

                    Console.WriteLine("Updated Successfully.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try

            End Sub
            ''' <summary>
            ''' Removes document properties of Doc file and creates output file
            ''' </summary> 
            Public Shared Sub RemoveDocumentProperties()
                Try
                    'ExStart:RemoveBuiltinDocumentPropertiesDocFormat
                    ' initialize Docformat
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    'Clean metadata
                    docFormat.CleanMetadata()

                    ' save output file...
                    docFormat.Save(Common.MapDestinationFilePath(filePath))

                    'ExEnd:RemoveBuiltinDocumentPropertiesDocFormat


                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub

#End Region

#Region "working with custom properties"
            ''' <summary>
            ''' Adds custom property in Doc file and creates output file
            ''' </summary> 
            Public Shared Sub AddCustomProperty()
                Try
                    'ExStart:AddCustomPropertyDocFormat
                    ' initialize DocFormat
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    ' initialize DocMetadata
                    Dim metadata As DocMetadata = docFormat.DocumentProperties


                    Dim propertyName As String = "New Custom Property"
                    Dim propertyValue As String = "123"

                    ' add boolean key
                    If Not metadata.ContainsKey(propertyName) Then
                        ' add property
                        metadata.Add(propertyName, propertyValue)
                    End If

                    ' save file in destination folder
                    docFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:AddCustomPropertyDocFormat


                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Gets custom properties of Doc file
            ''' </summary>
            Public Shared Sub GetCustomProperties()
                Try
                    'ExStart:GetCustomPropertiesDocFormat
                    ' initialize DocFormat
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    ' initialize metadata
                    Dim docMetadata As DocMetadata = docFormat.DocumentProperties

                    ' get properties  
                    Console.WriteLine(vbLf & "Custom Properties")
                    For Each keyValuePair As KeyValuePair(Of String, PropertyValue) In docMetadata
                        ' check if property is not built-in
                        If Not docMetadata.IsBuiltIn(keyValuePair.Key) Then
                            Try
                                ' get property value
                                Dim propertyValue As PropertyValue = docMetadata(keyValuePair.Key)
                                Console.WriteLine("Key: {0}, Type:{1}, Value: {2}", keyValuePair.Key, propertyValue.Type, propertyValue)
                            Catch
                            End Try
                        End If
                        'ExEnd:GetCustomPropertiesDocFormat
                    Next
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Removes custom properties of Doc file and creates output file
            ''' </summary> 
            Public Shared Sub RemoveCustomProperties()
                Try
                    'ExStart:RemoveCustomPropertyDocFormat
                    ' initialize DocFormat
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    ' initialize DocMetadata
                    Dim metadata As DocMetadata = docFormat.DocumentProperties

                    Dim propertyName As String = "New Custom Property"

                    ' check if property is not built-in
                    If metadata.ContainsKey(propertyName) Then
                        If Not metadata.IsBuiltIn(propertyName) Then
                            ' remove property

                            metadata.Remove(propertyName)
                        Else
                            Console.WriteLine("Can not remove built-in property.")
                        End If
                    Else
                        Console.WriteLine("Property does not exist.")
                    End If

                    Dim isexist As Boolean = metadata.ContainsKey(propertyName)

                    ' save file in destination folder
                    docFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:RemoveCustomPropertyDocFormat

                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Clears custom properties of Doc file and creates output file
            ''' </summary> 
            Public Shared Sub ClearCustomProperties()
                Try
                    'ExStart:ClearCustomPropertyDocFormat
                    ' initialize DocFormat
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    ' use one of the following methods
                    ' method:1 - clear custom properties 
                    docFormat.ClearCustomProperties()

                    ' method:2 - clear custom properties 
                    docFormat.DocumentProperties.ClearCustomData()

                    ' save file in destination folder
                    docFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:ClearCustomPropertyDocFormat

                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
#End Region

#Region "working with document comments"
            ''' <summary>
            ''' Gets document comments of Doc file
            ''' </summary> 
            Public Shared Sub GetDocumentComments()
                Try

                    ' initialize DocFormat
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    'get comments...
                    Dim comments As DocComment() = docFormat.ExtractComments()

                    'get commnets by author...
                    'DocComment[] comments = docFormat.ExtractComments("USMAN");

                    ' display comments
                    For Each comment As DocComment In comments
                        Console.WriteLine("Author: ", comment.Author)
                        Console.WriteLine("Created on Date: ", comment.CreatedDate)
                        Console.WriteLine("Initials: ", comment.Initials)
                        Console.WriteLine(vbLf)
                    Next
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Removes document comments of Doc file  
            ''' </summary> 
            Public Shared Sub RemoveComments()
                Try

                    ' initialize DocFormat
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    ' remove comments
                    docFormat.ClearComments()

                    ' save file in destination folder
                    docFormat.Save(Common.MapDestinationFilePath(filePath))



                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub

            ''' <summary>
            ''' Updates document comments of Doc file  
            ''' </summary> 
            Public Shared Sub UpdateComments()
                Try
                    'ExStart:UpdateDocumentComment
                    ' initialize DocFormat
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    ' extract comments
                    Dim comments As DocComment() = docFormat.ExtractComments()

                    If comments.Length > 0 Then
                        ' get first comment if exist
                        Dim comment = comments(0)

                        ' change comment's author
                        comment.Author = "Jack London"

                        ' change comment's text
                        comment.Text = "This comment is created using GroupDocs.Metadata"

                        ' update comment
                        docFormat.UpdateComment(comment.Id, comment)
                    End If

                    ' save file in destination folder
                    docFormat.Save(Common.MapDestinationFilePath(filePath))

                    'ExEnd:UpdateDocumentComment

                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
#End Region

#Region "working with pages and words"
            ''' <summary>
            ''' Gets word count and page count of Doc file
            ''' </summary> 
            Public Shared Sub GetWordAndPageCount()
                Try

                    ' initialize DocFormat
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    ' Get words count...
                    Dim wordsCount As Integer = docFormat.GetWordsCount()

                    ' Get pages count...
                    Dim pageCounts As Integer = docFormat.GetPagesCount()

                    Console.WriteLine("Words: {0}", wordsCount)


                    Console.WriteLine("Pages: {0}", pageCounts)
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
#End Region

#Region "working with hidden fields"
            ''' <summary>
            ''' Gets comments, merge fields and hidden fields of Doc file
            ''' </summary> 
            Public Shared Sub GetHiddenData()
                Try
                    'ExStart:GetHiddenDataInDocument
                    ' initialize DocFormat
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    ' inspect document
                    Dim inspectionResult As DocInspectionResult = docFormat.InspectDocument()

                    ' display comments
                    If inspectionResult.Comments.Length > 0 Then
                        Console.WriteLine("Comments in document:")
                        For Each comment As DocComment In inspectionResult.Comments
                            Console.WriteLine("Comment: {0}", comment.Text)
                            Console.WriteLine("Author: {0}", comment.Author)
                            Console.WriteLine("Date: {0}", comment.CreatedDate)
                        Next
                    End If

                    ' display merge fields
                    If inspectionResult.Fields.Length > 0 Then
                        Console.WriteLine(vbLf & "Merge Fields in document:")
                        For Each field As DocField In inspectionResult.Fields
                            Console.WriteLine(field.Name)
                        Next
                    End If

                    ' display hidden fields 
                    If inspectionResult.HiddenText.Length > 0 Then
                        Console.WriteLine(vbLf & "Hiddent text in document:")
                        For Each word As String In inspectionResult.HiddenText
                            Console.WriteLine(word)
                        Next
                        'ExEnd:GetHiddenDataInDocument

                    End If
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Gets comments, merge fields and hidden fields of Doc file
            ''' </summary> 
            Public Shared Sub RemoveMergeFields()
                Try
                    'ExStart:RemoveHiddenDataInDocument
                    ' initialize DocFormat
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    ' inspect document
                    Dim inspectionResult As DocInspectionResult = docFormat.InspectDocument()

                    ' if merge fields are presented
                    If inspectionResult.Fields.Length > 0 Then
                        ' remove it
                        docFormat.RemoveHiddenData(New DocInspectionOptions(DocInspectorOptionsEnum.Fields))

                        ' save file in destination folder
                        docFormat.Save(Common.MapDestinationFilePath(filePath))
                    End If
                    'ExEnd:RemoveHiddenDataInDocument

                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
#End Region

#Region "Working with Original File Docs"
            ''' <summary>
            '''  Save Changes after updating metadata of specific format
            ''' </summary>
            Public Shared Sub SaveFileAfterMetadataUpdate()
                'ExStart:SaveFileAfterMetadataUpdate
                ' initialize DocFormat
                Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                ' update document properties
                docFormat.DocumentProperties.Author = "Joe Doe"
                docFormat.DocumentProperties.Company = "Aspose"

                ' and commit changes
                docFormat.Save()
                'ExEnd:SaveFileAfterMetadataUpdate
            End Sub

            ''' <summary>
            '''  Throw an Exception for Protected Document
            ''' </summary>
            Public Shared Sub DocumentProtectedException()
                'ExStart:DocumentProtectedException
                ' initialize DocFormat
                Try
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    ' and try to get document properties
                    Dim documentProperties = docFormat.DocumentProperties
                Catch ex As DocumentProtectedException
                    Console.WriteLine("File is protected by password PDF: {0}", ex.Message)
                End Try
                'ExEnd:DocumentProtectedException
            End Sub
#End Region






            '''<summary>
            '''Reads calculated document info for MS Word format
            '''</summary>
            Public Shared Sub ReadDocumentInfo()
                'ExStart:ReadDocumentInfo
                ' initialize DocFormat
                Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                ' get document info
                Dim documentInfo As DocumentInfo = docFormat.DocumentInfo

                ' display characters count
                Dim charactersCount As Long = documentInfo.CharactersCount
                Console.WriteLine("Characters count: {0}", charactersCount)

                ' display pages count
                Dim pagesCount As Integer = documentInfo.PagesCount
                Console.WriteLine("Pages count: {0}", pagesCount)
                'ExEnd:ReadDocumentInfo 
            End Sub

            '''<summary>
            '''Displays file type of the Word document
            '''</summary>
            Public Shared Sub DisplayFileType()
                'ExStart:DisplayFileType
                ' initialize DocFormat
                Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                ' display file type
                Select Case docFormat.FileType
                    Case FileType.Doc
                        Console.WriteLine("Old binary document")
                        Exit Select

                    Case FileType.Docx
                        Console.WriteLine("XML-based document")
                        Exit Select
                End Select
                'ExEnd:DisplayFileType
            End Sub

            '''<summary>
            '''Reads Digital signatre in Word Document
            '''</summary>
            Public Shared Sub ReadDigitalSignature()
                'ExStart:ReadDigitalSignature
                ' initialize DocFormat
                Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                ' if document contains digital signatures
                If docFormat.HasDigitalSignatures Then
                    ' then inspect it
                    Dim inspectionResult = docFormat.InspectDocument()

                    ' and get digital signatures
                    Dim signatures As DigitalSignature() = inspectionResult.DigitalSignatures

                    For Each signature As DigitalSignature In signatures
                        ' get certificate subject
                        Console.WriteLine("Certificate subject: {0}", signature.CertificateSubject)

                        ' get certificate sign time
                        Console.WriteLine("Signed time: {0}", signature.SignTime)
                    Next
                End If
                'ExEnd:ReadDigitalSignature
            End Sub

            '''<summary>
            '''Removes digital signature from word document
            '''</summary>
            Public Shared Sub RemoveDigitalSignature()
                'ExStart:RemoveDigitalSignature
                ' initialize DocFormat
                Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                ' if document contains digital signatures
                If docFormat.HasDigitalSignatures Then
                    ' then remove them
                    docFormat.RemoveHiddenData(New DocInspectionOptions(DocInspectorOptionsEnum.DigitalSignatures))

                    ' and commit changes
                    docFormat.Save()
                End If
                'ExEnd:RemoveDigitalSignature
            End Sub





        End Class

        Public NotInheritable Class Pdf
            Private Sub New()
            End Sub
            ' initialize file path
            'ExStart:SourcePdfFilePath
            Private Const filePath As String = "Documents/Pdf/sample.pdf"
            'ExEnd:SourcePdfFilePath
#Region "working with builtin document properties"
            ''' <summary>
            ''' Gets builtin document properties of Pdf file  
            ''' </summary> 
            Public Shared Sub GetDocumentProperties()
                Try
                    'ExStart:GetBuiltinDocumentPropertyPdfFormat
                    ' initialize Pdfformat
                    Dim pdfFormat As New PdfFormat(Common.MapSourceFilePath(filePath))

                    ' initialize PdfMetadata
                    Dim pdfMetadata As PdfMetadata = pdfFormat.DocumentProperties

                    ' built-in properties
                    Console.WriteLine("Built-in Properties")
                    For Each [property] As KeyValuePair(Of String, PropertyValue) In pdfMetadata
                        ' check if built-in property
                        If pdfMetadata.IsBuiltIn([property].Key) Then
                            Console.WriteLine("{0} : {1}", [property].Key, [property].Value)
                        End If
                        'ExEnd:GetBuiltinDocumentPropertyPdfFormat
                    Next
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub


            ''' <summary>
            ''' Retrieves all XMP keys from PDF document  
            ''' </summary> 
            Public Shared Sub GetXMPPropertiesUsingKey(directoryPath As String)
                Try
                    'ExStart:GetXMPKeysPdfFormat
                    ' get PDF files only
                    Dim files As String() = Directory.GetFiles(Common.MapSourceFilePath(directoryPath), "*.pdf")

                    For Each path As String In files
                        ' try to get XMP metadata
                        Dim metadata As Mtdta.Metadata = MetadataUtility.ExtractSpecificMetadata(path, MetadataType.XMP)

                        ' skip if file does not contain XMP metadata
                        If metadata Is Nothing Then
                            Continue For
                        End If

                        ' cast to XmpMetadata
                        Dim xmpMetadata As XmpMetadata = TryCast(metadata, XmpMetadata)

                        ' and display all xmp keys
                        For Each key As String In xmpMetadata.Keys
                            Console.WriteLine(key)
                        Next
                    Next
                    'ExEnd:GetXMPKeysPdfFormat
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub


            ''' <summary>
            ''' Updates document properties of Pdf file and creates output file
            ''' </summary> 
            Public Shared Sub UpdateDocumentProperties()
                Try

                    'ExStart:UpdateBuiltinDocumentPropertyPdfFormat
                    ' initialize PdfFormat
                    Dim pdfFormat As New PdfFormat(Common.MapSourceFilePath(filePath))

                    ' initialize PdfMetadata
                    Dim pdfMetadata As PdfMetadata = pdfFormat.DocumentProperties

                    'update document property...
                    pdfMetadata.Author = "New author"
                    pdfMetadata.Subject = "New subject"
                    pdfMetadata.CreatedDate = DateTime.Now

                    'save output file...
                    pdfFormat.Save(Common.MapDestinationFilePath(filePath))

                    'ExEnd:UpdateBuiltinDocumentPropertyPdfFormat

                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try

            End Sub
            ''' <summary>
            ''' Removes document properties of Pdf file and creates output file
            ''' </summary> 
            Public Shared Sub RemoveDocumentProperties()
                Try
                    'ExStart:RemoveBuiltinDocumentPropertyPdfFormat
                    ' initialize PdfFormat
                    Dim pdfFormat As New PdfFormat(Common.MapSourceFilePath(filePath))

                    pdfFormat.CleanMetadata()

                    'save output file...
                    pdfFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:RemoveBuiltinDocumentPropertyPdfFormat



                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
#End Region

#Region "working with custom properties"
            ''' <summary>
            ''' Adds custom property in Pdf file and creates output file
            ''' </summary> 
            Public Shared Sub AddCustomProperty()
                Try
                    'ExStart:AddCustomDocumentPropertyPdfFormat
                    ' initialize PdfFormat
                    Dim pdfFormat As New PdfFormat(Common.MapSourceFilePath(filePath))

                    ' initialize PdfMetadata
                    Dim metadata As PdfMetadata = pdfFormat.DocumentProperties

                    Dim propertyName As String = "New Custom Property"
                    Dim propertyValue As String = "123"


                    ' check if property already exists
                    If Not metadata.ContainsKey(propertyName) Then
                        ' add property
                        metadata.Add(propertyName, propertyValue)
                    End If

                    ' save file in destination folder
                    pdfFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:AddCustomDocumentPropertyPdfFormat


                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Gets custom properties of Pdf file
            ''' </summary>
            Public Shared Sub GetCustomProperties()
                Try
                    'ExStart:GetCustomDocumentPropertiesPdfFormat
                    ' initialize Pdfformat
                    Dim pdfFormat As New PdfFormat(Common.MapSourceFilePath(filePath))

                    ' initialize PdfMetadata
                    Dim pdfMetadata As PdfMetadata = pdfFormat.DocumentProperties

                    Console.WriteLine(vbLf & "Custom Properties")
                    For Each keyValuePair As KeyValuePair(Of String, PropertyValue) In pdfMetadata
                        ' check if property is not built-in
                        If Not pdfMetadata.IsBuiltIn(keyValuePair.Key) Then
                            ' get property value
                            Dim propertyValue As PropertyValue = pdfMetadata(keyValuePair.Key)
                            Console.WriteLine("Key: {0}, Type:{1}, Value: {2}", keyValuePair.Key, propertyValue.Type, propertyValue)
                        End If
                        'ExEnd:GetCustomDocumentPropertiesPdfFormat
                    Next
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Removes custom property of Pdf file and creates output file
            ''' </summary> 
            Public Shared Sub RemoveCustomProperties()
                Try
                    'ExStart:RemoveCustomDocumentPropertiesPdfFormat
                    ' initialize PdfFormat
                    Dim pdfFormat As New PdfFormat(Common.MapSourceFilePath(filePath))

                    ' initialize PdfMetadata
                    Dim metadata As PdfMetadata = pdfFormat.DocumentProperties

                    Dim propertyName As String = "New Custom Property"

                    ' check if property is not built-in
                    If Not metadata.IsBuiltIn(propertyName) Then
                        ' remove property
                        metadata.Remove(propertyName)
                    Else
                        Console.WriteLine("Can not remove built-in property.")
                    End If

                    ' save file in destination folder
                    pdfFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:RemoveCustomDocumentPropertiesPdfFormat

                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Clears custom properties of Pdf file and creates output file
            ''' </summary> 
            Public Shared Sub ClearCustomProperties()
                Try
                    'ExStart:ClearCustomPropertyPdfFormat
                    ' initialize PdfFormat
                    Dim PdfFormat As New PdfFormat(Common.MapSourceFilePath(filePath))

                    ' use one of the following methods
                    ' method:1 - clear custom properties 
                    PdfFormat.ClearCustomProperties()

                    ' method:2 - clear custom properties
                    PdfFormat.DocumentProperties.ClearCustomData()

                    ' save file in destination folder
                    PdfFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:ClearCustomPropertyPdfFormat

                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
#End Region

#Region "working with XMP data"
            ''' <summary>
            ''' Gets XMP Data of Pdf file  
            ''' </summary> 
            Public Shared Sub GetXMPProperties()
                Try
                    'ExStart:GetXMPPropertiesPdfFormat
                    ' initialize Pdfformat
                    Dim pdfFormat As New PdfFormat(Common.MapSourceFilePath(filePath))

                    ' get pdf schema
                    Dim pdfPackage As PdfPackage = pdfFormat.XmpValues.Schemes.Pdf

                    Console.WriteLine("Keywords: {0}", pdfPackage.Keywords)
                    Console.WriteLine("PdfVersion: {0}", pdfPackage.PdfVersion)
                    'ExEnd:GetXMPPropertiesPdfFormat
                    Console.WriteLine("Producer: {0}", pdfPackage.Producer)
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub

            ''' <summary>
            ''' Updates XMP Data of Pdf file  
            ''' </summary> 
            Public Shared Sub UpdateXMPProperties()
                Try
                    'ExStart:UpdateXMPPropertiesPdfFormat
                    ' initialize Pdfformat
                    Dim pdfFormat As New PdfFormat(Common.MapSourceFilePath(filePath))

                    ' get pdf schema
                    Dim pdfPackage As PdfPackage = pdfFormat.XmpValues.Schemes.Pdf

                    ' update keywords
                    pdfPackage.Keywords = "literature, programming"

                    ' update pdf version
                    pdfPackage.PdfVersion = "1.0"

                    ' pdf:Producer could not be updated
                    'pdfPackage.Producer="";

                    'save output file...
                    'ExEnd:UpdateXMPPropertiesPdfFormat
                    pdfFormat.Save(Common.MapDestinationFilePath(filePath))
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
#End Region

#Region "Working with Hidden Data"
            Public Shared Sub RemoveHiddenData()
                Try
                    'ExStart:RemoveHiddenDataPdfFormat
                    ' initialize Pdfformat
                    Dim pdfFormat As New PdfFormat(Common.MapSourceFilePath(filePath))

                    ' inspect document
                    Dim inspectionResult As PdfInspectionResult = pdfFormat.InspectDocument()

                    ' get annotations
                    Dim annotation As PdfAnnotation() = inspectionResult.Annotations

                    ' if annotations are presented
                    If annotation.Length > 0 Then
                        ' remove all annotation
                        pdfFormat.RemoveHiddenData(New PdfInspectionOptions(PdfInspectorOptionsEnum.Annotations))

                        'save output file...
                        pdfFormat.Save(Common.MapDestinationFilePath(filePath))
                        'ExEnd:RemoveHiddenDataPdfFormat
                    End If
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
#End Region


            ''' <summary>
            ''' Loads Only Existing Metadata Keys into PdfMetadata Class
            ''' Feature is supported by version 17,03 or greater 
            ''' </summary>
            Public Shared Sub LoadExistingMetadataKeys()
                Try
                    'ExStart:LoadExistingMetadataKeys
                    ' initialize PdfFormat
                    Dim pdfFormat As New PdfFormat(Common.MapSourceFilePath(filePath))

                    ' get pdf properties
                    Dim properties As PdfMetadata = pdfFormat.DocumentProperties

                    ' go through Keys property and display related PDF properties
                    For Each key As String In properties.Keys
                        Console.WriteLine("[{0}]={1}", key, properties(key))
                        'ExEnd:LoadExistingMetadataKeys
                    Next
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            End Sub


        End Class

        Public NotInheritable Class Ppt
            Private Sub New()
            End Sub
            ' initialize file path
            'ExStart:SourcePptFilePath
            Private Const filePath As String = "Documents/Ppt/sample.pptx"
            'ExEnd:SourcePptFilePath
#Region "working with builtin document properties"
            ''' <summary>
            ''' Gets builtin document properties of Ppt file  
            ''' </summary> 
            Public Shared Sub GetDocumentProperties()
                Try
                    ' ExStart:GetBuiltinDocumentPropertiesPptFormat
                    ' initialize PptFormat
                    Dim pptFormat As New PptFormat(Common.MapSourceFilePath(filePath))

                    ' initialize PptMetadata
                    Dim pptMetadata As PptMetadata = pptFormat.DocumentProperties

                    ' built-in properties
                    Console.WriteLine(vbLf & "Built-in Properties")
                    For Each [property] As KeyValuePair(Of String, PropertyValue) In pptMetadata
                        If pptMetadata.IsBuiltIn([property].Key) Then
                            Console.WriteLine("{0} : {1}", [property].Key, [property].Value)
                        End If
                        'ExEnd:GetBuiltinDocumentPropertiesPptFormat
                    Next
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Updates document properties of Ppt file and creates output file
            ''' </summary> 
            Public Shared Sub UpdateDocumentProperties()
                Try

                    'ExStart:UpdateBuiltinDocumentPropertiesPptFormat
                    ' initialize PptFormat
                    Dim pptFormat As New PptFormat(Common.MapSourceFilePath(filePath))

                    ' initialize PptMetadata
                    Dim pptMetadata As PptMetadata = pptFormat.DocumentProperties

                    ' update document property
                    pptMetadata.Author = "New author"
                    pptMetadata.Subject = "New subject"
                    pptMetadata.Manager = "Usman"

                    ' set content type
                    pptMetadata.ContentType = "content type"

                    ' set hyperlink base
                    pptMetadata.HyperlinkBase = "http://groupdocs.com"

                    ' mark as shared
                    pptMetadata.SharedDoc = True

                    'save output file...
                    pptFormat.Save(Common.MapDestinationFilePath(filePath))
                    Console.WriteLine("File saved in destination folder.")
                    'ExEnd:UpdateBuiltinDocumentPropertiesPptFormat
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try

            End Sub
            ''' <summary>
            ''' Removes document properties of Ppt file and creates output file
            ''' </summary> 
            Public Shared Sub RemoveDocumentProperties()
                Try
                    'ExStart:RemoveBuiltinDocumentPropertiesPptFormat
                    ' initialize PptFormat
                    Dim pptFormat As New PptFormat(Common.MapSourceFilePath(filePath))

                    ' clean metadata
                    pptFormat.CleanMetadata()

                    ' save output file...
                    pptFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:RemoveBuiltinDocumentPropertiesPptFormat


                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub


            ''' <summary>
            ''' Reads document properties of Ppt file in an improved and fast way
            ''' </summary> 
            Public Shared Sub ImprovedMetadataReading()
                'ExStart:ImprovedMetadataReadingPpt
                'initialize ppt format
                Dim ppt As New PptFormat(Common.MapSourceFilePath(filePath))
                'use faster way to read metadata properties of ppt document
                Dim properties = ppt.DocumentProperties
                Console.WriteLine(properties)
                'ExEnd:ImprovedMetadataReadingPpt
            End Sub

#End Region

#Region "working with custom properties"
            ''' <summary>
            ''' Adds custom property in Ppt file and creates output file
            ''' </summary> 
            Public Shared Sub AddCustomProperty()
                Try
                    'ExStart:AddCustomDocumentPropertiesPptFormat
                    ' initialize PptFormat
                    Dim pptFormat As New PptFormat(Common.MapSourceFilePath(filePath))

                    ' initialize PptMetadata
                    Dim metadata As PptMetadata = pptFormat.DocumentProperties

                    ' set property details 
                    Dim propertyName As String = "New custom property"
                    Dim propertyValue As String = "Value"

                    ' check if property already exists
                    If Not metadata.ContainsKey(propertyName) Then
                        ' add property
                        metadata.Add(propertyName, propertyValue)
                    End If

                    ' save file in destination folder
                    pptFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:AddCustomDocumentPropertiesPptFormat


                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Gets custom properties of Ppt file  
            ''' </summary> 
            Public Shared Sub GetCustomProperties()
                Try
                    'ExStart:GetCustomDocumentPropertiesPptFormat
                    ' initialize PptFormat
                    Dim pptFormat As New PptFormat(Common.MapSourceFilePath(filePath))

                    ' initialize PptMetadata
                    Dim pptMetadata As PptMetadata = pptFormat.DocumentProperties

                    Console.WriteLine(vbLf & "Custom Properties")
                    For Each keyValuePair As KeyValuePair(Of String, PropertyValue) In pptMetadata
                        ' check if property is not built-in
                        If Not pptMetadata.IsBuiltIn(keyValuePair.Key) Then
                            ' get property value
                            Dim propertyValue As PropertyValue = pptMetadata(keyValuePair.Key)
                            Console.WriteLine("Key: {0}, Type:{1}, Value: {2}", keyValuePair.Key, propertyValue.Type, propertyValue)
                        End If
                        'ExEnd:GetCustomDocumentPropertiesPptFormat
                    Next
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Removes custom property of Ppt file and creates output file
            ''' </summary> 
            Public Shared Sub RemoveCustomProperties()
                Try
                    'ExStart:RemoveCustomDocumentPropertiesPptFormat
                    ' initialize PptFormat
                    Dim pptFormat As New PptFormat(Common.MapSourceFilePath(filePath))

                    ' initialize PptMetadata
                    Dim metadata As PptMetadata = pptFormat.DocumentProperties

                    Dim propertyName As String = "New custom property"

                    ' check if property is not built-in
                    If Not metadata.IsBuiltIn(propertyName) Then
                        ' remove property
                        metadata.Remove(propertyName)
                    Else
                        Console.WriteLine("Can not remove built-in property.")
                    End If

                    ' save file in destination folder
                    pptFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:RemoveCustomDocumentPropertiesPptFormat

                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Clears custom properties of Ppt file and creates output file
            ''' </summary> 
            Public Shared Sub ClearCustomProperties()
                Try
                    'ExStart:ClearCustomPropertyPptFormat
                    ' initialize PptFormat
                    Dim PptFormat As New PptFormat(Common.MapSourceFilePath(filePath))

                    ' use one of the following methods
                    ' method:1 - clear custom properties
                    PptFormat.ClearCustomProperties()

                    ' method:2 - clear custom properties 
                    PptFormat.DocumentProperties.ClearCustomData()

                    ' save file in destination folder
                    PptFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:ClearCustomPropertyPptFormat

                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
#End Region

#Region "working with hidden fields"
            ''' <summary>
            ''' Gets Comments, and Hidden Slides of PowerPoint file
            ''' </summary> 
            Public Shared Sub GetHiddenData()
                Try
                    'ExStart:GetHiddenDataInPPT
                    ' initialize PptFormat
                    Dim pptFormat As New PptFormat(Common.MapSourceFilePath(filePath))

                    ' get hidden data
                    Dim hiddenData As PptInspectionResult = pptFormat.InspectDocument()

                    ' get comments
                    Dim comments As PptComment() = hiddenData.Comments

                    ' get slides
                    Dim slides As PptSlide() = hiddenData.HiddenSlides

                    For Each comment As PptComment In comments
                        Console.WriteLine("Author: {0}, Slide: {1}", comment.Author, comment.SlideId)
                        'ExEnd:GetHiddenDataInPPT
                    Next
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub

            ''' <summary>
            ''' Removes Comments, and Hidden Slides of PowerPoint file
            ''' </summary> 
            Public Shared Sub RemoveHiddenData()
                Try
                    'ExStart:RemoveHiddenDataInPPT
                    ' initialize PptFormat
                    Dim pptFormat As New PptFormat(Common.MapSourceFilePath(filePath))

                    ' get hidden data
                    Dim hiddenData As PptInspectionResult = pptFormat.InspectDocument()

                    ' get comments
                    Dim comments As PptComment() = hiddenData.Comments

                    If comments.Length > 0 Then
                        ' remove all comments
                        pptFormat.RemoveHiddenData(New PptInspectionOptions(PptInspectorOptionsEnum.Comments))
                        Console.WriteLine("Hidden sheets removed.")

                        ' and commit changes
                        pptFormat.Save()
                        Console.WriteLine("Changes saved successfully!")
                    Else
                        Console.WriteLine("No comments found!")
                        'ExEnd:RemoveHiddenDataInPPT

                    End If
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
#End Region
        End Class

        Public NotInheritable Class Xls
            Private Sub New()
            End Sub
            ' initialize file path
            'ExStart:SourceXlsFilePath
            Private Const filePath As String = "Documents/Xls/sample.xls"
            'ExEnd:SourceXlsFilePath

            'initialize output file path
            'ExStart:DestinationXlsFilePath
            Private Const outputFilePath As String = "Documents/Xls/metadata-xls.xls"
            Private Const outputFilePathWithHiddenData As String = "Documents/Xls/hidden-data.xls"
            'ExEnd:DestinationXlsFilePath

#Region "working with builtin document properties"
            ''' <summary>
            ''' Gets builtin document properties of Xls file  
            ''' </summary> 
            Public Shared Sub GetDocumentProperties()
                Try

                    'ExStart:GetBuiltinDocumentPropertiesXlsFormat
                    ' initialize XlsFormat
                    Dim xlsFormat As New XlsFormat(Common.MapSourceFilePath(filePath))

                    ' initialize XlsMetadata
                    Dim xlsMetadata As XlsMetadata = xlsFormat.DocumentProperties

                    ' built-in properties
                    Console.WriteLine(vbLf & "Built-in Properties")
                    For Each [property] As KeyValuePair(Of String, PropertyValue) In xlsMetadata
                        ' check if property is biltin
                        If xlsMetadata.IsBuiltIn([property].Key) Then
                            Console.WriteLine("{0} : {1}", [property].Key, [property].Value)
                        End If
                        'ExEnd:GetBuiltinDocumentPropertiesXlsFormat
                    Next
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Updates document properties of Xls file and creates output file
            ''' </summary> 
            Public Shared Sub UpdateDocumentProperties()
                Try
                    'ExStart:UpdateBuiltinDocumentPropertiesXlsFormat
                    ' initialize XlsFormat
                    Dim xlsFormat As New XlsFormat(Common.MapSourceFilePath(filePath))

                    ' initialize XlsMetadata
                    Dim xlsMetadata As XlsMetadata = xlsFormat.DocumentProperties

                    'update document property...
                    xlsMetadata.Author = "New author"
                    xlsMetadata.Subject = "New subject"

                    'save output file...
                    xlsFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:UpdateBuiltinDocumentPropertiesXlsFormat

                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try

            End Sub
            ''' <summary>
            ''' Removes document properties of Xls file and creates output file
            ''' </summary> 
            Public Shared Sub RemoveDocumentProperties()
                Try
                    'ExStart:RemoveBuiltinDocumentPropertiesXlsFormat
                    ' initialize XlsFormat
                    Dim xlsFormat As New XlsFormat(Common.MapSourceFilePath(filePath))

                    ' clean metadata
                    xlsFormat.CleanMetadata()

                    'save output file...
                    xlsFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:RemoveBuiltinDocumentPropertiesXlsFormat


                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
#End Region

#Region "working with custom properties"
            ''' <summary>
            ''' Adds custom property in Xls file and creates output file
            ''' </summary> 
            Public Shared Sub AddCustomProperty()
                Try
                    'ExStart:AddCustomDocumentPropertiesXlsFormat
                    ' initialize XlsFormat
                    Dim xlsFormat As New XlsFormat(Common.MapSourceFilePath(filePath))

                    ' initialize XlsMetadata
                    Dim metadata As XlsMetadata = xlsFormat.DocumentProperties

                    Dim propertyName As String = "New Custom Property"
                    Dim propertyValue As String = "123"

                    ' check if property already exists
                    If Not metadata.ContainsKey(propertyName) Then
                        ' add property
                        metadata.Add(propertyName, propertyValue)
                    End If

                    ' save file in destination folder
                    xlsFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:AddCustomDocumentPropertiesXlsFormat


                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Gets custom properties of Xls file  
            ''' </summary> 
            Public Shared Sub GetCustomProperties()
                Try
                    'ExStart:GetCustomDocumentPropertiesXlsFormat
                    ' initialize XlsFormat
                    Dim xlsFormat As New XlsFormat(Common.MapSourceFilePath(filePath))

                    ' initialize XlsMetadata
                    Dim xlsMetadata As XlsMetadata = xlsFormat.DocumentProperties

                    Console.WriteLine(vbLf & "Custom Properties")
                    For Each keyValuePair As KeyValuePair(Of String, PropertyValue) In xlsMetadata
                        ' check if property is not built-in
                        If Not xlsMetadata.IsBuiltIn(keyValuePair.Key) Then
                            ' get property value
                            Dim propertyValue As PropertyValue = xlsMetadata(keyValuePair.Key)
                            Console.WriteLine("Key: {0}, Type:{1}, Value: {2}", keyValuePair.Key, propertyValue.Type, propertyValue)
                        End If
                        'ExEnd:GetCustomDocumentPropertiesXlsFormat
                    Next
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Removes custom property of Xls file and creates output file
            ''' </summary> 
            Public Shared Sub RemoveCustomProperties()
                Try
                    'ExStart:RemoveCustomDocumentPropertiesXlsFormat
                    ' initialize XlsFormat
                    Dim xlsFormat As New XlsFormat(Common.MapSourceFilePath(filePath))

                    ' initialize XlsMetadata
                    Dim metadata As XlsMetadata = xlsFormat.DocumentProperties

                    Dim propertyName As String = "New Custom Property"

                    ' check if property is not built-in
                    If Not metadata.IsBuiltIn(propertyName) Then
                        ' remove property
                        metadata.Remove(propertyName)
                    Else
                        Console.WriteLine("Can not remove built-in property.")
                    End If

                    ' save file in destination folder
                    xlsFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:RemoveCustomDocumentPropertiesXlsFormat

                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Clears custom properties of Xls file and creates output file
            ''' </summary> 
            Public Shared Sub ClearCustomProperties()
                Try
                    'ExStart:ClearCustomPropertyXlsFormat
                    ' initialize XlsFormat
                    Dim xlsFormat As New XlsFormat(Common.MapSourceFilePath(filePath))

                    ' use one of the following methods
                    ' method:1 - clear custom properties
                    xlsFormat.ClearCustomProperties()

                    ' method:2 - clear custom properties
                    xlsFormat.DocumentProperties.ClearCustomData()

                    ' save file in destination folder
                    xlsFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:ClearCustomPropertyXlsFormat

                    Console.WriteLine("File saved in destination folder.")
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
#End Region
#Region "working with hidden fields"
            ''' <summary>
            ''' Gets comments and hidden sheets of Xls file
            ''' </summary> 
            Public Shared Sub GetHiddenData()
                Try
                    'ExStart:GetHiddenDataInXls
                    ' initialize XlsFormat
                    Dim xlsFormat As New XlsFormat(Common.MapSourceFilePath(filePath))

                    ' get hidden data
                    Dim hiddenData As XlsInspectionResult = xlsFormat.InspectDocument()

                    ' get hidden sheets
                    Dim hiddenSheets As XlsSheet() = hiddenData.HiddenSheets

                    ' get comments
                    Dim comments As XlsComment() = hiddenData.Comments

                    If comments.Length > 0 Then
                        For Each comment As XlsComment In comments
                            Console.WriteLine("Comment: {0}, Column: {1}", comment.ToString(), comment.Column)
                        Next
                    Else
                        Console.WriteLine("No comment found!")
                        'ExEnd:GetHiddenDataInXls
                    End If
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
            ''' <summary>
            ''' Removes hidden data of Xls file
            ''' </summary> 
            Public Shared Sub RemoveHiddenData()
                Try
                    'ExStart:RemoveHiddenDataInXls
                    ' initialize XlsFormat
                    Dim xlsFormat As New XlsFormat(Common.MapSourceFilePath(filePath))

                    ' get hidden data
                    Dim hiddenData As XlsInspectionResult = xlsFormat.InspectDocument()

                    ' get hidden sheets
                    Dim hiddenSheets As XlsSheet() = hiddenData.HiddenSheets


                    ' display hidden fields 
                    If hiddenSheets.Length > 0 Then
                        ' clear hidden sheets
                        xlsFormat.RemoveHiddenData(New XlsInspectionOptions(XlsInspectorOptionsEnum.HiddenSheets))
                        Console.WriteLine("Hidden sheets removed.")

                        ' and commit changes
                        xlsFormat.Save()
                        Console.WriteLine("Changes save successfully!")
                    Else
                        Console.WriteLine("No sheets found.")
                        'ExEnd:RemoveHiddenDataInXls
                    End If
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
#End Region

#Region "working with content type document properties"
            ''' <summary>
            ''' Gets content type document properties of Xls file  
            ''' </summary> 
            Public Shared Sub GetContentTypeDocumentProperties()
                Try
                    'ExStart:GetContentTypeDocumentPropertiesXlsFormat
                    ' initialize XlsFormat
                    Dim xlsFormat As New XlsFormat(Common.MapSourceFilePath(filePath))

                    ' get xls properties
                    Dim xlsProperties As XlsMetadata = xlsFormat.DocumentProperties

                    ' get content properties
                    Dim contentProperties As XlsContentProperty() = xlsProperties.ContentProperties

                    For Each [property] As XlsContentProperty In contentProperties
                        Console.WriteLine("Property: {0}, value: {1}, type: {2}", [property].Name, [property].Value, [property].PropertyType)

                    Next
                    'ExEnd:GetContentTypeDocumentPropertiesXlsFormat
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
#End Region

#Region "working with exporting document properties"
            ''' <summary>
            ''' Shows how to export content type properties to csv/excel
            ''' </summary>
            Public Shared Sub ContentTypePropertiesExport()
                Try
                    'ExStart:ContentTypePropertiesExportXls
                    ' export to excel
                    Dim content As Byte() = ExportFacade.ExportToExcel(Common.MapSourceFilePath(filePath))

                    ' write data to the file
                    File.WriteAllBytes(Common.MapDestinationFilePath(outputFilePath), content)
                    'ExEnd:ContentTypePropertiesExportXls
                    Console.WriteLine("file has been exported")
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try

            End Sub

            ''' <summary>
            ''' shows how to add content type property
            ''' </summary>
            Public Shared Sub AddContentTypeProperty()
                Try
                    'ExStart:AddContentTypePropertyXls
                    ' initialize XlsFormat
                    Dim xlsFormat As New XlsFormat(Common.MapSourceFilePath(filePath))

                    ' get all xls properties
                    Dim xlsProperties As XlsMetadata = xlsFormat.DocumentProperties

                    ' if Excel contains content type properties
                    If xlsProperties.ContentTypeProperties.Length > 0 Then
                        ' than remove all content type properties
                        xlsProperties.ClearContentTypeProperties()
                    End If

                    ' set hidden field
                    xlsProperties.AddContentTypeProperty("user hidden id", "asdk12dkvjdjh3")

                    ' and commit changes
                    xlsFormat.Save(Common.MapDestinationFilePath(outputFilePathWithHiddenData))
                    'ExEnd:AddContentTypePropertyXls
                    Console.WriteLine("file has been exported")

                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try

            End Sub


#End Region

            ''' <summary>
            ''' Reads thumnail in excel file, since while using Excel format document may be empty. In this case need to check if thumbnail is not empty
            ''' Feature is supported by version 17.3 or greater
            ''' </summary>
            Public Shared Sub ReadThumbnailXls()
                Try
                    'ExStart:ReadThumbnailXls
                    ' initialize XlsFormat
                    Dim docFormat As New XlsFormat(Common.MapSourceFilePath(filePath))

                    ' get thumbnail
                    Dim thumbnailData As Byte() = docFormat.Thumbnail

                    ' check if first sheet is empty
                    If thumbnailData.Length = 0 Then
                        Console.WriteLine("Excel sheet is empty and does not contain data")
                    Else
                        ' write thumbnail to PNG image since it has png format
                        File.WriteAllBytes(Common.MapDestinationFilePath("xlsThumbnail.png"), thumbnailData)
                        'ExEnd:ReadThumbnailXls
                    End If
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            End Sub



        End Class

        Public NotInheritable Class OneNote
            Private Sub New()
            End Sub
            ' initialize file path
            'ExStart:SourceOneNoteFilePath
            Private Const filePath As String = "Documents/OneNote/sample.one"
            'ExEnd:SourceOneNoteFilePath

            ''' <summary>
            ''' Gets metadata of OneNote file  
            ''' </summary> 
            Public Shared Sub GetMetadata()
                Try

                    'ExStart:GetMetadataOneNoteFormat
                    ' initialize OneNoteFormat
                    Dim oneNoteFormat As New OneNoteFormat(Common.MapSourceFilePath(filePath))

                    ' get metadata
                    Dim oneNoteMetadata = oneNoteFormat.GetMetadata()
                    If oneNoteFormat IsNot Nothing Then
                        ' get IsFixedSize 
                        Console.WriteLine("IsFixedSize: {0}", oneNoteMetadata.IsFixedSize)
                        ' get IsReadOnly 
                        Console.WriteLine("IsReadOnly: {0}", oneNoteMetadata.IsReadOnly)
                        ' get IsSynchronized 
                        Console.WriteLine("IsSynchronized: {0}", oneNoteMetadata.IsSynchronized)
                        ' get Length 
                        Console.WriteLine("Length: {0}", oneNoteMetadata.Length)
                        ' get Rank 
                        Console.WriteLine("Rank: {0}", oneNoteMetadata.Rank)
                        'ExEnd:GetMetadataOneNoteFormat
                    End If
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub

            ''' <summary>
            ''' Gets pages of OneNote file  
            ''' </summary> 
            Public Shared Sub GetPagesInfo()
                Try

                    'ExStart:GetPagesOneNoteFormat
                    ' initialize OneNoteFormat
                    Dim oneNoteFormat As New OneNoteFormat(Common.MapSourceFilePath(filePath))

                    ' get pages
                    Dim pages As OneNotePageInfo() = oneNoteFormat.GetPages()

                    For Each info As OneNotePageInfo In pages
                        ' get Author 
                        Console.WriteLine("Author: {0}", info.Author)
                        ' get CreationTime 
                        Console.WriteLine("CreationTime: {0}", info.CreationTime)
                        ' get LastModifiedTime 
                        Console.WriteLine("LastModifiedTime: {0}", info.LastModifiedTime)
                        ' get Title 
                        Console.WriteLine("Title: {0}", info.Title)

                        Console.WriteLine(vbLf & vbLf)
                        'ExEnd:GetPagesOneNoteFormat
                    Next
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub


        End Class


        Public NotInheritable Class MSProject
            Private Sub New()
            End Sub
            ' initialize file path
            'ExStart:SourceMSProjectFilePath
            Private Const filePath As String = "Documents/MSProject/sample.mpp"
            'ExEnd:SourceMSProjectFilePath

            ''' <summary>
            ''' Gets properties of MS Project file  
            ''' </summary> 
            Public Shared Sub GetMetadata()
                Try
                    'ExStart:GetMetadataMppFormat
                    ' initialize MppFormat
                    Dim mppFormat As New MppFormat(Common.MapSourceFilePath(filePath))

                    ' get document properties
                    Dim properties As MppMetadata = mppFormat.GetProperties()

                    If mppFormat IsNot Nothing Then
                        ' get Author 
                        Console.WriteLine("Author: {0}", properties.Author)
                        ' get Company 
                        Console.WriteLine("Company: {0}", properties.Company)
                        ' get Keywords 
                        Console.WriteLine("Keywords: {0}", properties.Keywords)
                        'ExEnd:GetMetadataMppFormat
                    End If
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub
        End Class

        Public NotInheritable Class MSVisio
            Private Sub New()
            End Sub
            ' initialize file path
            'ExStart:SourceMSProjectFilePath
            Private Const filePath As String = "Documents/MSVisio/sample.vdx"
            'ExEnd:SourceMSProjectFilePath

            ''' <summary>
            ''' Gets properties of MS Visio file  
            ''' </summary> 
            Public Shared Sub GetMetadata()
                Try
                    'ExStart:GetMetadataMppFormat
                    ' initialize MppFormat
                    Dim mppFormat As New MppFormat(Common.MapSourceFilePath(filePath))

                    ' get document properties
                    Dim properties As MppMetadata = mppFormat.GetProperties()

                    If mppFormat IsNot Nothing Then
                        ' get Author 
                        Console.WriteLine("Author: {0}", properties.Author)
                        ' get Company 
                        Console.WriteLine("Company: {0}", properties.Title)
                        'ExEnd:GetMetadataMppFormat
                    End If
                Catch exp As Exception
                    Console.WriteLine(exp.Message)
                End Try
            End Sub


            ''' <summary>
            ''' Sets properties of MS Project file  
            ''' </summary> 
            Public Shared Sub SetProperties()
                ' initialize VisioFormat
                Dim visioFormat As New VisioFormat(Common.MapSourceFilePath(filePath))

                ' update creator
                visioFormat.DocumentProperties.Creator = "John Doe"

                ' update title
                visioFormat.DocumentProperties.Title = "Example Title"

                ' commit changes
                visioFormat.Save()

                Console.WriteLine("Creator: {0}: ", visioFormat.DocumentProperties.Creator)
                Console.WriteLine("Title: {0}: ", visioFormat.DocumentProperties.Title)
            End Sub
        End Class


        Public NotInheritable Class ODT
            Private Sub New()
            End Sub
            'initialize file path
            'ExStart:SourceODTProjectFilePath
            Private Const filePath As String = "Documents/Odt/sample.odt"
            'ExEnd:SourceODTProjectFilePath

            ''' <summary>
            ''' Gets properties of Open Document Format file  
            ''' </summary> 
            Public Shared Sub GetOdtMetadata()
                Try
                    'ExStart:ReadOdtMetadata
                    ' initialize DocFormat with ODT file's path
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))


                    ' read all metadata properties
                    Dim metadata As Mtdta.Metadata = docFormat.DocumentProperties

                    ' and display them
                    For Each [property] As MetadataProperty In metadata
                        Console.WriteLine([property])
                    Next
                    'ExEnd:ReadOdtMetadata
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
            End Sub


            ''' <summary>
            ''' Updates properties of Open Document Format file  
            ''' </summary> 
            Public Shared Sub UpdateOdtMetadata()
                Try
                    'ExStart:UpdateOdtMetadata
                    ' initialize DocFormat with ODT file's path
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    ' initialize DocMetadata
                    Dim docMetadata As DocMetadata = docFormat.DocumentProperties

                    'update document property...
                    docMetadata.Author = "Rida "
                    docMetadata.Company = "Aspose"
                    docMetadata.Manager = "Rida Fatima"

                    'save output file...
                    docFormat.Save(Common.MapDestinationFilePath(filePath))
                    'ExEnd:UpdateOdtMetadata
                    Console.WriteLine("Updated Successfully.")
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try


            End Sub

        End Class



        ''' <summary>
        ''' Detects document protection
        ''' </summary> 
        Public Shared Sub DetectProtection(filePath As String)
            Try
                'ExStart:DetectProtection
                Dim format As FormatBase = FormatFactory.RecognizeFormat(Common.MapSourceFilePath(filePath))

                If format.Type.ToString().ToLower() = "doc" Then
                    ' initialize DocFormat
                    Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                    ' determines whether document is protected by password
                    Console.WriteLine(If(docFormat.IsProtected, "Document is protected", "Document is protected"))
                ElseIf format.Type.ToString().ToLower() = "pdf" Then
                    ' initialize DocFormat
                    Dim pdfFormat As New PdfFormat(Common.MapSourceFilePath(filePath))

                    ' determines whether document is protected by password
                    Console.WriteLine(If(pdfFormat.IsProtected, "Document is protected", "Document is protected"))
                ElseIf format.Type.ToString().ToLower() = "xls" Then
                    ' initialize DocFormat
                    Dim xlsFormat As New XlsFormat(Common.MapSourceFilePath(filePath))

                    ' determines whether document is protected by password
                    Console.WriteLine(If(xlsFormat.IsProtected, "Document is protected", "Document is protected"))
                ElseIf format.Type.ToString().ToLower() = "ppt" Then
                    ' initialize DocFormat
                    Dim pptFormat As New PptFormat(Common.MapSourceFilePath(filePath))

                    ' determines whether document is protected by password
                    Console.WriteLine(If(pptFormat.IsProtected, "Document is protected", "Document is protected"))
                Else
                    Console.WriteLine("Invalid Format.")
                    'ExEnd:DetectProtection
                End If
            Catch exp As Exception
                Console.WriteLine("Exception occurred: " + exp.Message)
            End Try

        End Sub
        ''' <summary>
        ''' Detects document format at runtime 
        ''' Enhancement in version 1.7
        ''' </summary>

        Public Shared Sub RuntimeFormatDetection(directoryPath As String)
            Try
                'string directoryPath = @"C:\\download files";
                Dim files As String() = Directory.GetFiles(Common.MapSourceFilePath(directoryPath))

                For Each path__1 As String In files
                    Dim metadata As Global.GroupDocs.Metadata.Metadata = MetadataUtility.ExtractSpecificMetadata(path__1, MetadataType.Document)
                    ' check if file has document format
                    If metadata Is Nothing Then
                        Continue For
                    End If

                    Console.WriteLine("File: {0}" & vbLf, Path.GetFileName(path__1))

                    Dim values As IEnumerable(Of KeyValuePair(Of [String], PropertyValue)) = DirectCast(metadata, IEnumerable(Of KeyValuePair(Of [String], PropertyValue)))

                    For Each keyValuePair As KeyValuePair(Of String, PropertyValue) In values
                        Console.WriteLine("Metadata: {0}, value: {1}", keyValuePair.Key, keyValuePair.Value)
                    Next

                    Console.WriteLine(vbLf & "**************************************************" & vbLf)
                Next
            Catch exp As Exception
                Console.WriteLine("Exception occurred: " + exp.Message)
            End Try

        End Sub


        ''' <summary>
        ''' Demonstrates how to read thumbnail in documents, here we are using Word document
        ''' Feature is supported in version 17.03  or greater
        ''' </summary>
        ''' <param name="filePath"></param>
        Public Shared Sub ReadThumbnail(filePath As String)
            Try
                'ExStart:ReadThumbnail
                ' initialize DocFormat
                Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                ' get thumbnail
                Dim thumbnailData As Byte() = docFormat.Thumbnail

                ' write thumbnail to PNG image since it has png format
                'ExEnd:ReadThumbnail
                File.WriteAllBytes(Common.MapDestinationFilePath("thumbnail.png"), thumbnailData)
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Sub

        ''' <summary>
        ''' Loads DocumentInfo property in DocumentFormat using lazy loading pattern
        ''' Feature is supported by version 17.03 or greater
        ''' </summary>
        ''' <param name="filePath"></param>
        Public Shared Sub LazyLoadDocumentInfoProperty(filePath As String)
            Try
                'ExStart:LazyLoadDocumentInfoProperty
                ' initialize DocFormat
                Dim docFormat As New DocFormat(Common.MapSourceFilePath(filePath))

                ' get document info
                Dim documentInfo As DocumentInfo = docFormat.DocumentInfo

                ' next call returns previous documentInfo object
                'ExEnd:LazyLoadDocumentInfoProperty
                Dim [next] As DocumentInfo = docFormat.DocumentInfo
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try

        End Sub


    End Class
End Namespace