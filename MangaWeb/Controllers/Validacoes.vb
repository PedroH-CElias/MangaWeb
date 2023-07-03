Imports System.Data.SqlClient
Public Class Validacoes

    ' Verifica se a o Email é válido
    Public Function ValidarEmail(email As String) As Boolean
        Dim padrao As String = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"
        Dim regex As New Regex(padrao)
        Dim match As Match = regex.Match(email)
        Return match.Success
    End Function

    ' Verifica se a o Email ja existe
    Public Function ValidarEmailExistente(email As String)

        Dim Server = "DESKTOP-CSTV6N9" 'Alterar de acordo com o servidor local
        Dim dataBase = "MangaTec" 'Alterar de acordo com o nome do banco de dados
        Dim user = ""
        Dim senha = ""

        Dim connectionString As String = $"Data Source={Server}; Integrated Security=true;Initial Catalog={dataBase};user ID={user};Password={senha}"

        Dim sql = "SELECT COUNT(*) FROM Usuario WHERE email=@Email "
        Dim result As Boolean
        Try
            Using cn = New SqlConnection(connectionString)
                cn.Open()

                Using cmd = New SqlCommand(sql, cn)
                    cmd.Parameters.AddWithValue("@Email", email)

                    Dim cont As Integer = Convert.ToInt32(cmd.ExecuteScalar())

                    If cont > 0 Then
                        result = True
                    Else
                        result = False
                    End If

                End Using
            End Using
        Catch ex As Exception
            MsgBox("Falha!")
            result = True
        End Try

        Return result
    End Function

    ' Verifica se a o CPF é válido
    Public Function ValidarCpf(Cpf As String) As Boolean
        Dim CpfValido = True
        Dim I As Integer, J As Byte, N1 As Integer, N2 As Integer

        If Len(Cpf) <> 11 Then
            CpfValido = False
        ElseIf Cpf = StrDup(11, "0") Or Cpf = StrDup(11, "1") Or Cpf = StrDup(11, "2") Or Cpf = StrDup(11, "3") Or Cpf = StrDup(11, "4") Or Cpf = StrDup(11, "5") Or Cpf = StrDup(11, "6") Or Cpf = StrDup(11, "7") Or Cpf = StrDup(11, "8") Or Cpf = StrDup(11, "9") Then
            CpfValido = False
        Else
            For I = 1 To Len(Cpf)
                If Not IsNumeric(Mid(Cpf, I, 1)) Then
                    CpfValido = False
                    Exit For
                End If
            Next
        End If

        If CpfValido Then
            N1 = 0
            J = 1
            For I = 10 To 2 Step -1
                N1 += Val(Mid(Cpf, J, 1)) * I
                J += 1
            Next
            N1 = (N1 * 10) Mod 11
            If N1 = 10 Then
                N1 = 0
            End If
            If N1 <> Mid(Cpf, 10, 1) Then
                CpfValido = False
            End If

            If CpfValido Then
                N2 = 0
                J = 1
                For I = 11 To 2 Step -1
                    N2 += Val(Mid(Cpf, J, 1)) * I
                    J += 1
                Next
                N2 = (N2 * 10) Mod 11 'o resto da divisão
                If N2 = 10 Then
                    N2 = 0
                End If
                If N2 <> Mid(Cpf, 11, 1) Then 'posição 11 (última)
                    CpfValido = False
                End If
            End If

        End If

        ValidarCpf = CpfValido
    End Function

    ' Verifica se a nova senha é válida
    Public Function ValidaSenha(password As String) As Boolean

        ' Verifica se a senha tem pelo menos 6 caracteres
        If password.Length < 6 Then
            Return False
        End If

        ' Verifica se a senha contém pelo menos uma letra maiúscula
        If Not password.Any(Function(c) Char.IsUpper(c)) Then
            Return False
        End If

        ' Verifica se a senha contém pelo menos um número
        If Not password.Any(Function(c) Char.IsDigit(c)) Then
            Return False
        End If

        Return True
    End Function
End Class
