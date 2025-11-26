import { AuthUser } from "../types/AuthUser";
import { AuthClient, createAuthClient, AuthState, UserLoadedListener } from "./authClient";
import { AuthErrorDetails } from "../types/AuthErrorDetails";
import { OidcService } from "./oidcService";


class AuthService {

    protected userManager: AuthClient = createAuthClient();
    private userLoadedListeners = new Set<UserLoadedListener>();

    async initAsync(): Promise<AuthState> {
        this.userManager.events.addUserLoaded(this.onUserLoaded.bind(this))
        this.userManager.events.addUserUnloaded(this.onUserUnloaded.bind(this));
        this.userManager.events.addSilentRenewError(this.signoutAsync.bind(this));

        let user = await this.userManager.getUser();

        return {
            user: user,
            isAuthenticated: !!user && !user.expired,
        }
    }

    async signinAsync(): Promise<AuthErrorDetails | void> {
        try {
            const returnTo = getCurrentpath();
            sessionStorage.setItem('returnTo', returnTo); // fallback pós-login
            await this.userManager.signinRedirect({ state: { returnTo } });

        } catch (error) {
            console.error('Error during signin redirect:', error);
            return this.buildAuthErrorDetails(error)
        }
    }

    async signinCallbackAsync(): Promise<AuthErrorDetails | void> {
        try {
            const loggedUser = await this.userManager.signinRedirectCallback();
            this.redirectReturnTo(loggedUser);
        } catch (error) {
            return this.buildAuthErrorDetails(error)
        }
    }

    async signoutAsync(): Promise<AuthErrorDetails | void> {
        try {
            await this.userManager.signoutRedirect({ state: { returnTo: '/' } });
        } catch (error) {
            return this.buildAuthErrorDetails(error)
        }
    }


    addUserLoaded(listener: UserLoadedListener): () => void {
        this.userLoadedListeners.add(listener);
        return () => this.userLoadedListeners.delete(listener);
    }

    private onUserLoaded(user: AuthUser) {
        const listeners = Array.from(this.userLoadedListeners);
        for (const l of listeners) {
            try {
                l(user);
            } catch (err) {
                // Boa prática: não deixe um listener quebrar os outros
                console.error('Erro em listener de userLoaded:', err);
            }
        }
    };

    private onUserUnloaded() {
        const listeners = Array.from(this.userLoadedListeners);
        for (const l of listeners) {
            try {
                l(null);
            } catch (err) {
                // Boa prática: não deixe um listener quebrar os outros
                console.error('Erro em listener de userLoaded:', err);
            }
        }
    }

    private redirectReturnTo(loggedUser: AuthUser) {
        const state = (loggedUser?.state as any) || {};
        const returnTo: string = state?.returnTo || sessionStorage.getItem('returnTo') || '/';

        sessionStorage.removeItem('returnTo');

        // evita loop em rotas de auth
        if (returnTo.startsWith('/login') ||
            returnTo.startsWith('/callback') ||
            returnTo.startsWith('/logout')) {
            window.location.replace('/');
        } else {
            window.location.replace(returnTo);
        }
    }


    private buildAuthErrorDetails(err: unknown): AuthErrorDetails {
        const anyErr = err as any;
        const url = OidcService.getOidcParamsFromUrl();

        const code =
            anyErr?.error ??
            url.error ??
            (anyErr?.name === 'TypeError' ? 'network_error' : null);

        const description =
            anyErr?.error_description ?? url.error_description ?? anyErr?.message ?? null;

        const errorUri = anyErr?.error_uri ?? url.error_uri ?? null;
        const state = anyErr?.state ?? null;
        const traceId = state?.traceId ?? sessionStorage.getItem('oidc:lastTraceId') ?? null;
        const requestId = state?.requestId ?? null;

        return {
            title: code === 'login_required' ? 'Sua sessão expirou' : 'Não foi possível processar sua solicitação',
            description,
            code,
            errorUri,
            traceId,
            requestId,
            timestamp: new Date().toISOString(),
            authority: this.userManager.settings.authority,
            clientId: this.userManager.settings.client_id ?? null,
            redirectUri: this.userManager.settings.redirect_uri ?? null,
        };
    }
}


export default new AuthService();

function getCurrentpath() {
    const pathWithQuery = window.location.pathname + window.location.search;
    return pathWithQuery;
}