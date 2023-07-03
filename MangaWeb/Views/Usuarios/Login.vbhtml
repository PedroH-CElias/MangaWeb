@Imports System.Data.SqlClient
@Code

    ViewData("Title") = "Login"

    Dim server = "DESKTOP-CSTV6N9" 'Alterar de acordo com o servidor local
    Dim dataBase = "MangaTec" 'Alterar de acordo com o nome do banco de dados
    Dim user = ""
    Dim senha = ""

    Dim connectionString As String = $"Data Source={server}; Integrated Security=true;Initial Catalog={dataBase};user ID={user};Password={senha}"
    Try
        Dim email As String = Request.Form("email")
        Dim password As String = Request.Form("password")


        Dim query As String = "SELECT COUNT(*) FROM Usuario WHERE email = '" & email & "' AND senha = '" & password & "'"

        Dim result As Integer = 0

        Using conn As New SqlConnection(connectionString)
            Using cmd As New SqlCommand(query, conn)

                conn.Open()

                result = Convert.ToInt32(cmd.ExecuteScalar())

                If result > 0 Then
                    ' Login bem-sucedido
                    Dim idSelecionado As String = "SELECT id FROM Usuario WHERE email = '" & email & "' AND senha = '" & password & "'"

                    Using cmd2 As New SqlCommand(idSelecionado, conn)

                        result = Convert.ToInt32(cmd2.ExecuteScalar())


                        Session("userid") = result
                        Session("userSenha") = password

                        Response.Redirect("Logado")

                    End Using

                End If

            End Using
        End Using
    Catch ex As Exception
        ViewBag.ErrorMessage = "Erro ao conectar ao banco de dados: " & ex.Message
    End Try



End Code

<!DOCTYPE html>
<html>
<head>
  
    <title>Formulário de Login</title>
    <style>
        .login-container {
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            background-color: #f2f2f2;
            font-family: Arial, sans-serif;
        }

        .login-form {
            background-color: #fff;
            text-align: center;
            padding: 20px;
            border-radius: 4px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            max-width: 400px;
            width: 100%;
        }

            .login-form h2 {
                text-align: center;
                margin-bottom: 20px;
            }

        .form-group {
            margin-bottom: 15px;
        }

            .form-group label {
                display: block;
                margin-bottom: 5px;
                font-weight: bold;
            }

            .form-group input {
                width: 100%;
                padding: 8px;
                border: 1px solid #ccc;
                border-radius: 4px;
            }

                .form-group input[type="submit"] {
                    background-color: #4CAF50;
                    color: white;
                    cursor: pointer;
                }

                    .form-group input[type="submit"]:hover {
                        background-color: #45a049;
                    }

        .forgot-password {
            text-align: center;
        }
        
    </style>
</head>
    <body>
   

        <div class="login-container">
            <div class="login-form">
                <h2>Login</h2>

                <form method="post">
                    <div class="form-group">
                        <label for="email">E-mail:</label>
                        <input type="text" id="email" name="email" required>
                    </div>
                    <div class="form-group">
                        <label for="password">Senha:</label>
                        <input type="password" id="password" name="password" required>
                    </div>
                    <br/>
                    <br/>
                    <div class="form-group">
                        <input type="submit" value="Entrar">
                    </div>
   
                </form>

                <p class="forgot-password">
                    @Html.ActionLink("Não possui cadastro?", "Create", "Usuarios")
                </p>
            </div>
        </div>

    </body>
</html>
