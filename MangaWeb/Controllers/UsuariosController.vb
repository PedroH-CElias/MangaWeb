
Imports System.Data.Entity

Imports System.Net

Imports System.Data.SqlClient



Namespace Controllers
    Public Class UsuariosController
        Inherits System.Web.Mvc.Controller
        Private validacoes As New Validacoes

        Private db As New MangaTecEntities

        Function Login() As ActionResult
            Return View()
        End Function

        Function Logado() As ActionResult
            Return View()
        End Function

        Function Index() As ActionResult
            Return View()
        End Function

        ' GET: Usuarios/Create
        Function Create() As ActionResult
            Return View()
        End Function

        ' POST: Usuarios/Create
        'To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        'more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="id,email,senha,cpf,nome,idade,profissao")> ByVal usuario As Usuario) As ActionResult
            If ValidarForm(usuario) = True Then
                If ModelState.IsValid Then
                    db.Usuario.Add(usuario)
                    db.SaveChanges()
                    Return RedirectToAction("Login", "Usuarios")
                End If

            End If

            Return View(usuario)

        End Function

        Public Function ValidarForm(usuario As Usuario) As Boolean
            Dim result = True
            If validacoes.ValidarEmail(usuario.email) = False Then
                Response.Write("<br/><font color=""red"">E-mail inválido!</font>")
                RedirectToAction("Create", "Usuarios")
                result = False
            ElseIf validacoes.ValidarEmailExistente(usuario.email) = True Then
                Response.Write("<br/><font color=""red"">E-mail já cadastrado!</font>")
                RedirectToAction("Create", "Usuarios")
                result = False
            ElseIf validacoes.ValidaSenha(usuario.senha) = False Then
                Response.Write("<br/><font color=""red"">Senha inválida! Ela deve conter no mínimo seis caracteres, uma letra maiúscula e um número!</font>")
                RedirectToAction("Create", "Usuarios")
                result = False
            ElseIf validacoes.ValidarCpf(usuario.cpf) = False Then
                Response.Write("<br/><font color=""red"">CPF inválido!</font>")
                RedirectToAction("Create", "Usuarios")
                result = False
            End If
            Return result
        End Function

        Public Function EditarSenha() As ActionResult
            Return View()
        End Function

        <HttpPost>
        Public Function AtualizarSenha(novaSenha As String, confirmarSenha As String) As ActionResult


            'ID do usuário atualmente logado 
            Dim userId As Integer = Session("userid")

            If validacoes.ValidaSenha(novaSenha) Then
                If novaSenha <> Session("userSenha") Then
                    If novaSenha = confirmarSenha Then


                        Dim server = "DESKTOP-CSTV6N9" 'Alterar de acordo com o servidor local
                        Dim dataBase = "MangaTec" 'Alterar de acordo com o nome do banco de dados
                        Dim user = ""
                        Dim senha = ""

                        Dim connectionString As String = $"Data Source={server}; Integrated Security=true;Initial Catalog={dataBase};user ID={user};Password={senha}"
                        Dim query As String = "UPDATE Usuario SET senha = '" & novaSenha & "' WHERE id = '" & userId & "'"

                        Using conn As New SqlConnection(connectionString)
                            Using cmd As New SqlCommand(query, conn)

                                conn.Open()
                                cmd.ExecuteNonQuery()
                            End Using
                        End Using

                        Return RedirectToAction("Login")

                    Else
                        Response.Write("<div style =""justify content:center; margin-top 5%;""><font color=""red"">As senhas não coincidem!</font><div/><br/>")

                        Return View("EditarSenha")
                    End If
                Else
                    Response.Write("<div style =""justify content:center; margin-top 5%;""><font color=""red"">A nova senha não pode ser igual a atual!</font><div/><br/>")

                    Return View("EditarSenha")
                End If
            Else
                Response.Write("<div style =""justify content:center; margin-top 5%;""><font color=""red"">A nova senha é inválida! Ela deve conter no mínimo:<br/> seis caracteres, uma letra maiúscula e um número!</font><div/><br/>")


                Return View("EditarSenha")
            End If



        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub
    End Class
End Namespace
