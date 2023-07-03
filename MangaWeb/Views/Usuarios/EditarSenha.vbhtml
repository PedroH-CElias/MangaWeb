@Code
    ViewData("Title") = "Editar Senha"




End Code

@Using (Html.BeginForm("AtualizarSenha", "Usuarios", FormMethod.Post, New With {.class = "form-style"}))
    @<p><strong>Nova senha:</strong></p>
    @Html.TextBox("novaSenha", Nothing, New With {.class = "input-style"})
    @<p><strong>Confirmar senha:</strong></p>
    @Html.TextBox("confirmarSenha", Nothing, New With {.class = "input-style"})
    @<input type="submit" value="Atualizar Senha" class="button-style" />
    @Html.ActionLink("Cancelar", "Logado", "Usuarios")


End Using

<style>
    body {
        display: flex;
        justify-content: center;
        align-items: center;
        height: 100vh;
        background-color: #f1f1f1;
        font-family: Arial, sans-serif;
    }

    .form-style {
        width: 300px;
        padding: 20px;
        background-color: #f1f1f1;
        border: 1px solid #ccc;
        border-radius: 4px;
    }

    .input-style {
        width: 100%;
        padding: 10px;
        margin-bottom: 10px;
        border: 1px solid #ccc;
        border-radius: 4px;
        box-sizing: border-box;
    }

    .button-style-red{
        background-color: red;
        color: white;
        padding: 10px 20px;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }

        .button-style-red:hover {
            background-color: darkred;
        }

    .button-style {
        background-color: #4CAF50;
        color: white;
        padding: 10px 20px;
        border: none;
        border-radius: 4px;
        cursor: pointer;
    }

        .button-style:hover {
            background-color: #45a049;
        }
</style>

