// alertas.js - Centraliza funções de SweetAlert2 para uso global

function getSwalThemeOptions() {
    // Obtém as variáveis CSS do tema atual
    const getVar = (name) => getComputedStyle(document.documentElement).getPropertyValue(name).trim();

    const isDark = document.documentElement.classList.contains('dark-theme');
    return {
        background: getVar('--card-bg'),
        color: getVar('--text')
    };
}

// Sucesso
function alertaSucesso(mensagem, titulo = 'Sucesso') {
    Swal.fire({
        icon: 'success',
        title: titulo,
        text: mensagem,
        ...getSwalThemeOptions()
    });
}

// Erro
function alertaErro(mensagem, titulo = 'Erro') {
    Swal.fire({
        icon: 'error',
        title: titulo,
        text: mensagem,
        ...getSwalThemeOptions()
    });
}

// Aviso
function alertaAviso(mensagem, titulo = 'Atenção') {
    Swal.fire({
        icon: 'warning',
        title: titulo,
        text: mensagem,
        ...getSwalThemeOptions()
    });
}

// Informação
function alertaInfo(mensagem, titulo = 'Informação') {
    Swal.fire({
        icon: 'info',
        title: titulo,
        text: mensagem,
        ...getSwalThemeOptions()
    });
}

// Confirmação (retorna Promise)
function alertaConfirmacao(mensagem, titulo = 'Confirmação', textoConfirmar = 'Sim', textoCancelar = 'Cancelar') {
    return Swal.fire({
        title: titulo,
        text: mensagem,
        icon: 'question',
        showCancelButton: true,
        confirmButtonText: textoConfirmar,
        cancelButtonText: textoCancelar,
        ...getSwalThemeOptions()
    });
}