(function () {
    const SIDEBAR_KEY = 'hc_sidebar_collapsed';
    const SIDEBAR_ID = 'appSidebar';
    const TOGGLE_ID = 'sidebarToggle';

    function salvarSidebarEstado(collapsed) {
        try {
            localStorage.setItem(SIDEBAR_KEY, collapsed ? '1' : '0');
        } catch (e) { /* ignore */ }
    }

    function obterSidebarEstado() {
        try {
            return localStorage.getItem(SIDEBAR_KEY) === '1';
        } catch (e) {
            return false;
        }
    }

    function aplicarSidebarEstado() {
        const sidebar = document.getElementById(SIDEBAR_ID);
        if (!sidebar) return;
        const collapsed = obterSidebarEstado();
        sidebar.classList.toggle('collapsed', collapsed);
    }

    function toggleSidebar() {
        const sidebar = document.getElementById(SIDEBAR_ID);
        if (!sidebar) return;
        const isCollapsed = sidebar.classList.toggle('collapsed');
        salvarSidebarEstado(isCollapsed);
    }

    // registra evento e inicializa após DOM pronto
    function init() {
        const btn = document.getElementById(TOGGLE_ID);
        if (btn) btn.addEventListener('click', function (e) { e.preventDefault(); toggleSidebar(); });

        aplicarSidebarEstado();

        // opcional: responde a mudança de tema (se precisar fazer ajustes JS)
        window.addEventListener('temaAlterado', function (ev) {
            // se precisar sincronizar algo ao trocar tema, adicione aqui
            // exemplo: console.log('tema alterado para', ev.detail.tema);
        });
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', init);
    } else {
        init();
    }

})();