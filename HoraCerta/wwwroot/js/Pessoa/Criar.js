// Funções em Português para controlar o comportamento do CEP sem usar classe/constructor

function initControladorCep(prefixo = 'Endereco') {
    const ids = {
        cep: `${prefixo}_CEP`,
        rua: `${prefixo}_Rua`,
        bairro: `${prefixo}_Bairro`,
        cidade: `${prefixo}_Cidade`,
        estado: `${prefixo}_Estado`,
        complemento: `${prefixo}_Complemento`,
        cepError: 'cep-error'
    };

    let timeout = null;

    function getEl(id) {
        return document.getElementById(id);
    }

    async function buscarCep(cep) {
        const rua = getEl(ids.rua);
        const bairro = getEl(ids.bairro);
        const cidade = getEl(ids.cidade);
        const estado = getEl(ids.estado);
        const complemento = getEl(ids.complemento);
        const cepError = getEl(ids.cepError);

        try {
            const res = await fetch(`/Pessoa/ConsultarCep/${cep}`);
            if (!res.ok) throw new Error('CEP não encontrado');
            const data = await res.json();
            if (data.erro) throw new Error('CEP inválido');

            if (rua) rua.value = data.logradouro ?? '';
            if (bairro) bairro.value = data.bairro ?? '';
            if (cidade) cidade.value = data.localidade ?? '';
            if (estado) estado.value = data.uf ?? '';
            if (complemento) complemento.value = data.complemento ?? '';
            if (cepError) cepError.textContent = '';
            if (rua) rua.focus();
        } catch (err) {
            limparCampos();
            // opcional: mostrar mensagem de erro
            // if (cepError) cepError.textContent = err.message;
        }
    }

    function limparCampos() {
        const rua = getEl(ids.rua);
        const bairro = getEl(ids.bairro);
        const cidade = getEl(ids.cidade);
        const estado = getEl(ids.estado);
        const complemento = getEl(ids.complemento);

        if (rua) rua.value = '';
        if (bairro) bairro.value = '';
        if (cidade) cidade.value = '';
        if (estado) estado.value = '';
        if (complemento) complemento.value = '';
    }

    function formatarCep(valor) {
        let v = (valor || '').replace(/\D/g, '');
        if (v.length > 8) v = v.slice(0, 8);
        return v.length > 5 ? `${v.slice(0, 5)}-${v.slice(5)}` : v;
    }

    function onCepInput(e) {
        const input = e.target;
        let v = (input.value || '').replace(/\D/g, '');
        if (v.length > 8) v = v.slice(0, 8);
        input.value = formatarCep(v);
        clearTimeout(timeout);
        if (v.length === 8) {
            timeout = setTimeout(() => buscarCep(v), 300);
        }
    }

    function onCepBlur(e) {
        const v = (e.target.value || '').replace(/\D/g, '');
        if (v.length === 8) buscarCep(v);
    }

    function ligarEventos() {
        const cepInput = getEl(ids.cep);
        if (!cepInput) return;
        cepInput.addEventListener('input', onCepInput);
        cepInput.addEventListener('blur', onCepBlur);
    }

    // inicializa imediatamente (após DOM pronto)
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', ligarEventos);
    } else {
        ligarEventos();
    }

    // expõe API mínima se quiser chamar manualmente
    return { buscarCep, limparCampos, formatarCep };
}

// inicializa padrão (prefixo 'Endereco')
initControladorCep();