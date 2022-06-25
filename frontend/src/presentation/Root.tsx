import { CssBaseline, ThemeProvider } from '@mui/material';
import { ReactKeycloakProvider } from '@react-keycloak/web';
import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import Router from './Router';
import theme from './theme';
import './i18n';
import { QueryClient, QueryClientProvider } from 'react-query';
import { ReactQueryDevtools } from 'react-query/devtools';
import keycloak from '../kc';
import { RecoilRoot } from 'recoil';
import { Toaster } from 'react-hot-toast';

const queryClient = new QueryClient();

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
				authClient={keycloak}
			>
				<QueryClientProvider client={queryClient}>
					<BrowserRouter>
						<ThemeProvider theme={theme}>
							{/* CssBaseline kickstart an elegant, consistent, and simple baseline to build upon. */}
							<CssBaseline />
							<Router />
							<Toaster />
							<ReactQueryDevtools initialIsOpen={false} />
						</ThemeProvider>
					</BrowserRouter>
				</QueryClientProvider>
			</ReactKeycloakProvider>
		</RecoilRoot>
	);
};
