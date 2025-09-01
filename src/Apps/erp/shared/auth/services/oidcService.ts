export const OidcService = {
    getOidcParamsFromUrl(href = window.location.href) {
        const url = new URL(href);
        const from = (sp: URLSearchParams) => ({
            error: sp.get("error"),
            error_description: sp.get("error_description"),
            error_uri: sp.get("error_uri"),
        });
        let p = from(url.searchParams);
        if (!p.error && url.hash.startsWith("#")) p = from(new URLSearchParams(url.hash.slice(1)));
        return p;
    },

    async checkIdpStatus(authority: string): Promise<string> {
        try {
            const res = await fetch(`${authority}/.well-known/openid-configuration`, { cache: "no-store" });
            return res.ok ? "OK" : `HTTP ${res.status}`;
        } catch (e: any) {
            // Diferencie um pouco (útil p/ suporte)
            if (e?.message?.includes("CORS")) return "CORS";
            return `OFFLINE (${e?.name ?? "Error"})`;
        }
    }
}
