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

// Máscaras em tempo real: telefone/whats ( (99) 9 9999-9999 ) e CPF (999.999.999-99)
(function () {
    'use strict';

    function onlyDigits(str) {
        return (str || '').replace(/\D/g, '');
    }

    function maskPhone(value) {
        const d = onlyDigits(value).slice(0, 11); // até 11 dígitos
        if (!d) return '';
        if (d.length <= 2) return '(' + d;
        const dArea = d.slice(0, 2);
        const rest = d.slice(2);
        let out = '(' + dArea + ') ';
        if (rest.length === 0) return out;
        out += rest[0]; // primeiro dígito (o '9' em celulares)
        if (rest.length === 1) return out;
        const part = rest.slice(1);
        if (part.length <= 4) return out + ' ' + part;
        return out + ' ' + part.slice(0, 4) + '-' + part.slice(4);
    }

    function maskCPF(value) {
        const d = onlyDigits(value).slice(0, 11);
        if (!d) return '';
        let s = d;
        s = s.replace(/^(\d{3})(\d)/, '$1.$2');
        s = s.replace(/^(\d{3}\.\d{3})(\d)/, '$1.$2');
        s = s.replace(/^(\d{3}\.\d{3}\.\d{3})(\d)/, '$1-$2');
        return s;
    }

    function applyMaskToInput(input) {
        const maskType = input.dataset.mask;
        if (!maskType) return;

        function listener(e) {
            const selectionStart = input.selectionStart;
            const oldLen = input.value.length;
            const before = input.value;
            let newValue = '';
            if (maskType === 'phone') newValue = maskPhone(input.value);
            else if (maskType === 'cpf') newValue = maskCPF(input.value);
            input.value = newValue;

            // tenta manter o caret em posição aproximada
            const newLen = newValue.length;
            const delta = newLen - oldLen;
            try {
                input.setSelectionRange(Math.max(0, selectionStart + delta), Math.max(0, selectionStart + delta));
            } catch (err) { /* alguns navegadores podem falhar */ }
        }

        input.addEventListener('input', listener, { passive: true });
        input.addEventListener('paste', function (ev) {
            // aplicar máscara após o paste
            setTimeout(function () { listener(); }, 0);
        });
        // inicializa valor formatado se já houver conteúdo
        if (input.value) {
            if (maskType === 'phone') input.value = maskPhone(input.value);
            if (maskType === 'cpf') input.value = maskCPF(input.value);
        }
    }

    function init() {
        let elsPhone = document.querySelectorAll('input[data-mask="phone"]');
        let elsCpf = document.querySelectorAll('input[data-mask="cpf"]');
        elsPhone.forEach(applyMaskToInput);
        elsCpf.forEach(applyMaskToInput);
    }

    if (document.readyState === 'loading') document.addEventListener('DOMContentLoaded', init);
    else init();

})();