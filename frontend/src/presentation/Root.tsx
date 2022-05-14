import { CssBaseline, ThemeProvider } from '@mui/material';
import { ReactKeycloakProvider } from '@react-keycloak/web';
import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import Router from './Router';
import theme from './theme';
import './i18n';
import keycloak from '../Keycloak';

export const Root: React.FC = () => {
	return (
		<ReactKeycloakProvider authClient={keycloak}>
			<BrowserRouter>
				<ThemeProvider theme={theme}>
					{/* CssBaseline kickstart an elegant, consistent, and simple baseline to build upon. */}
					<CssBaseline />
					<Router />
				</ThemeProvider>
			</BrowserRouter>
		</ReactKeycloakProvider>
	);
};
