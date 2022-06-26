import { CssBaseline, ThemeProvider } from '@mui/material';
import { ReactKeycloakProvider } from '@react-keycloak/web';
import Keycloak from 'keycloak-js';
import { ConfirmProvider } from 'material-ui-confirm';
import React from 'react';
import { Toaster } from 'react-hot-toast';
import { QueryClient, QueryClientProvider } from 'react-query';
import { ReactQueryDevtools } from 'react-query/devtools';
import { BrowserRouter } from 'react-router-dom';
import { RecoilRoot } from 'recoil';
import './i18n';
import Router from './Router';
import theme from './theme';

const queryClient = new QueryClient({
	defaultOptions: {
		queries: {
			retry: (failureCount, error: any) => {
				if (error.response?.status === 404) {
					return false;
				}

				return failureCount < 3;
			},
		},
	},
});

export const keycloakClient = new Keycloak({
	clientId: 'webapp-v1',
	realm: 'dev',
	url: 'https://idp.srm.k3s.devnico.cloud',
});

export const Root: React.FC = () => {
	const eventLogger = (event: unknown, error: unknown) => {
		console.log('onKeycloakEvent', event, error);
	};

	const tokenLogger = (token: any) => {
		if (token.token) {
			console.log('onKeycloakTokens', token);
			// cookies.remove('user_token');
			// cookies.set('user_token', token.token, { path: '/' });
			// dispatch(actions.setAuth(true));
		}
	};

	return (
		<RecoilRoot>
			<ReactKeycloakProvider
				initOptions={{ onLoad: 'login-required' }}
				onEvent={eventLogger}
				onTokens={tokenLogger}
				authClient={keycloakClient}
			>
				<QueryClientProvider client={queryClient}>
					<BrowserRouter>
						<ThemeProvider theme={theme}>
							<ConfirmProvider>
								{/* CssBaseline kickstart an elegant, consistent, and simple baseline to build upon. */}
								<CssBaseline />
								<Router />
								<Toaster
									position='top-right'
									reverseOrder={false}
								/>
								<ReactQueryDevtools initialIsOpen={false} />
							</ConfirmProvider>
						</ThemeProvider>
					</BrowserRouter>
				</QueryClientProvider>
			</ReactKeycloakProvider>
		</RecoilRoot>
	);
};
