
Imports System.Web.Services
Imports System.Data.SqlClient

Public Class ManageColors
    Inherits System.Web.UI.Page

    Private Shared connectionString As String = "Your DB Connection String Here"

    <WebMethod>
    Public Shared Function GetColors() As List(Of Object)
        Dim colors As New List(Of Object)()
        Using conn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand("SELECT * FROM Colors ORDER BY ColorDisplayOrder", conn)
            conn.Open()
            Dim reader As SqlDataReader = cmd.ExecuteReader()
            While reader.Read()
                colors.Add(New With {
                    .ColorID = reader("ColorID"),
                    .ColorName = reader("ColorName"),
					.ColorDisplayOrder = reader("ColorDisplayOrder"),
                    .ColorPrice = reader("ColorPrice"),
                    .ColorInStock = reader("ColorInStock")
                })
            End While
        End Using
        Return colors
    End Function

    <WebMethod>
    Public Shared Sub AddColor(ByVal color As Object)
        Using conn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand("INSERT INTO Colors (ColorName, ColorDisplayOrder, ColorPrice, ColorInStock) VALUES (@ColorName, @DisplayOrder, @Price,  @InStock)", conn)
            cmd.Parameters.AddWithValue("@ColorName", color("ColorName"))
			cmd.Parameters.AddWithValue("@DisplayOrder", color("ColorDisplayOrder"))
            cmd.Parameters.AddWithValue("@Price", color("ColorPrice"))
            cmd.Parameters.AddWithValue("@InStock", color("ColorInStock"))
            conn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    <WebMethod>
    Public Shared Sub DeleteColor(ByVal id As Integer)
        Using conn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand("DELETE FROM Colors WHERE ColorId = @ColorID", conn)
            cmd.Parameters.AddWithValue("@ColorID", id)
            conn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub
	
	<WebMethod>
    Public Shared Sub UpdateColor(ByVal color As Object)
        Using conn As New SqlConnection(connectionString)
            Dim cmd As New SqlCommand("UPDATE Colors SET ColorName = @ColorName, ColorDisplayOrder = @DisplayOrder, ColorPrice = @Price,  ColorInStock = @InStock WHERE ColorID = @ColorID", conn)
            cmd.Parameters.AddWithValue("@ColorID", color("ColorID"))
            cmd.Parameters.AddWithValue("@ColorName", color("ColorName"))
			cmd.Parameters.AddWithValue("@DisplayOrder", color("ColorDisplayOrder"))
            cmd.Parameters.AddWithValue("@Price", color("ColorPrice"))
            cmd.Parameters.AddWithValue("@InStock", color("ColorInStock"))
            conn.Open()
            cmd.ExecuteNonQuery()
        End Using
    End Sub


    <WebMethod>
    Public Shared Sub UpdateOrder(ByVal order As List(Of Object))
        Using conn As New SqlConnection(connectionString)
            conn.Open()
            For Each item In order
                Dim cmd As New SqlCommand("UPDATE Colors SET ColorDisplayOrder = @DisplayOrder WHERE ColorId = @ColorID", conn)
                cmd.Parameters.AddWithValue("@ColorID", item("ColorID"))
                cmd.Parameters.AddWithValue("@DisplayOrder", item("DisplayOrder"))
                cmd.ExecuteNonQuery()
            Next
        End Using
    End Sub

End Class