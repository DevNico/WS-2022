import { CssBaseline, ThemeProvider } from '@mui/material';
import { ReactKeycloakProvider } from '@react-keycloak/web';
import React, { useEffect } from 'react';
import { BrowserRouter } from 'react-router-dom';
import Router from './Router';
import theme from './theme';
import './i18n';
import { DefaultOptions, QueryClient, QueryClientProvider } from 'react-query';
import { ReactQueryDevtools } from 'react-query/devtools';
import { RecoilRoot, useSetRecoilState } from 'recoil';
import { Toaster } from 'react-hot-toast';
import Keycloak from 'keycloak-js';
import { ErrorResponse } from '../api/models';
import { ErrorType } from '../api/axios';

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
							{/* CssBaseline kickstart an elegant, consistent, and simple baseline to build upon. */}
							<CssBaseline />
							<Router />
							<Toaster
								position='top-right'
								reverseOrder={false}
							/>
							<ReactQueryDevtools initialIsOpen={false} />
						</ThemeProvider>
					</BrowserRouter>
				</QueryClientProvider>
			</ReactKeycloakProvider>
		</RecoilRoot>
	);
};
