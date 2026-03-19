// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// ==================================================
// SISTEMA DE TROCA DE TEMA (CLARO/ESCURO)
// ==================================================
(function () {
    'use strict';

    // ==================================================
    // CONFIGURAÇÕES
    // ==================================================
    const CONFIG = {
        chaveArmazenamento: 'hc_theme',
        idBotao: 'themeToggle',
        classeTemaDark: 'dark-theme',
        icones: {
            claro: '☀️',
            escuro: '🌙'
        }
    };

    // ==================================================
    // FUNÇÕES DE ARMAZENAMENTO
    // ==================================================

    /**
     * Salva o tema no localStorage
     * @param {string} tema - 'dark' ou 'light'
     */
    function salvarTema(tema) {
        try {
            localStorage.setItem(CONFIG.chaveArmazenamento, tema);
        } catch (erro) {
            console.warn('Erro ao salvar tema:', erro);
        }
    }

    /**
     * Recupera o tema salvo do localStorage
     * @returns {string|null} - 'dark', 'light' ou null
     */
    function obterTemaSalvo() {
        try {
            return localStorage.getItem(CONFIG.chaveArmazenamento);
        } catch (erro) {
            console.warn('Erro ao obter tema salvo:', erro);
            return null;
        }
    }

    /**
     * Verifica a preferência do sistema operacional
     * @returns {string} - 'dark' ou 'light'
     */
    function obterPreferenciaSistema() {
        if (window.matchMedia && window.matchMedia('(prefers-color-scheme: dark)').matches) {
            return 'dark';
        }
        return 'light';
    }

    /**
     * Obtém o tema preferido (prioriza salvo > sistema > padrão)
     * @returns {string} - 'dark' ou 'light'
     */
    function obterTemaPreferido() {
        const temaSalvo = obterTemaSalvo();

        if (temaSalvo === 'dark' || temaSalvo === 'light') {
            return temaSalvo;
        }

        return obterPreferenciaSistema();
    }

    // ==================================================
    // FUNÇÕES DE APLICAÇÃO DO TEMA
    // ==================================================

    /**
     * Aplica o tema na página
     * @param {string} tema - 'dark' ou 'light'
     */
    function aplicarTema(tema) {
        const elementoRaiz = document.documentElement;

        if (tema === 'dark') {
            elementoRaiz.classList.add(CONFIG.classeTemaDark);
        } else {
            elementoRaiz.classList.remove(CONFIG.classeTemaDark);
        }

        atualizarBotao(tema);
    }

    /**
     * Atualiza o ícone e estado do botão de troca de tema
     * @param {string} tema - 'dark' ou 'light'
     */
    function atualizarBotao(tema) {
        const botao = document.getElementById(CONFIG.idBotao);

        if (!botao) return;

        const icone = tema === 'dark' ? CONFIG.icones.escuro : CONFIG.icones.claro;
        const textoAcessibilidade = tema === 'dark' ? 'Ativar tema claro' : 'Ativar tema escuro';

        botao.textContent = icone;
        botao.setAttribute('aria-pressed', tema === 'dark');
        botao.setAttribute('aria-label', textoAcessibilidade);
        botao.setAttribute('title', textoAcessibilidade);
    }

    /**
     * Obtém o tema atual aplicado
     * @returns {string} - 'dark' ou 'light'
     */
    function obterTemaAtual() {
        const possuiClasseDark = document.documentElement.classList.contains(CONFIG.classeTemaDark);
        return possuiClasseDark ? 'dark' : 'light';
    }

    /**
     * Alterna entre tema claro e escuro
     */
    function alternarTema() {
        const temaAtual = obterTemaAtual();
        const novoTema = temaAtual === 'dark' ? 'light' : 'dark';

        salvarTema(novoTema);
        aplicarTema(novoTema);

        // Dispara evento customizado para outras partes da aplicação
        dispararEventoMudancaTema(novoTema);
    }

    /**
     * Define um tema específico
     * @param {string} tema - 'dark' ou 'light'
     */
    function definirTema(tema) {
        if (tema !== 'dark' && tema !== 'light') {
            console.error('Tema inválido. Use "dark" ou "light".');
            return;
        }

        salvarTema(tema);
        aplicarTema(tema);
        dispararEventoMudancaTema(tema);
    }

    // ==================================================
    // FUNÇÕES DE EVENTOS
    // ==================================================

    /**
     * Dispara evento customizado quando o tema é alterado
     * @param {string} novoTema - 'dark' ou 'light'
     */
    function dispararEventoMudancaTema(novoTema) {
        const evento = new CustomEvent('temaAlterado', {
            detail: { tema: novoTema }
        });
        window.dispatchEvent(evento);
    }

    /**
     * Configura o listener do botão de alternância
     */
    function configurarBotao() {
        const botao = document.getElementById(CONFIG.idBotao);

        if (!botao) {
            console.warn(`Botão com id "${CONFIG.idBotao}" não encontrado.`);
            return;
        }

        botao.addEventListener('click', function (evento) {
            evento.preventDefault();
            alternarTema();
        });
    }

    /**
     * Monitora mudanças na preferência do sistema
     */
    function monitorarPreferenciaSistema() {
        if (!window.matchMedia) return;

        const mediaQuery = window.matchMedia('(prefers-color-scheme: dark)');

        mediaQuery.addEventListener('change', function (evento) {
            // Só aplica automaticamente se usuário não tiver preferência salva
            const temaSalvo = obterTemaSalvo();

            if (!temaSalvo) {
                const novoTema = evento.matches ? 'dark' : 'light';
                aplicarTema(novoTema);
                dispararEventoMudancaTema(novoTema);
            }
        });
    }

    // ==================================================
    // INICIALIZAÇÃO
    // ==================================================

    /**
     * Inicializa o sistema de temas
     */
    function inicializar() {
        // Aplica tema inicial
        const temaInicial = obterTemaPreferido();
        aplicarTema(temaInicial);

        // Configura interações
        configurarBotao();
        monitorarPreferenciaSistema();

        console.log('✅ Sistema de temas inicializado');
    }

    // ==================================================
    // API PÚBLICA
    // ==================================================

    /**
     * Expõe funções para uso externo
     */
    window.HoraCertaTema = {
        alternar: alternarTema,
        definir: definirTema,
        obter: obterTemaAtual,
        aplicarPreferido: function () {
            aplicarTema(obterTemaPreferido());
        }
    };

    // ==================================================
    // EXECUÇÃO
    // ==================================================

    // Aguarda DOM estar pronto
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', inicializar);
    } else {
        inicializar();
    }



})();