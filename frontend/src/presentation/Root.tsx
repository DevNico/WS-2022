import { CssBaseline, ThemeProvider } from '@mui/material';
import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import Router from './Router';
import theme from './theme';
import './i18n';

export const Root: React.FC = () => {
	return (
		<BrowserRouter>
			<ThemeProvider theme={theme}>
				{/* CssBaseline kickstart an elegant, consistent, and simple baseline to build upon. */}
				<CssBaseline />
				<Router />
			</ThemeProvider>
		</BrowserRouter>
	);
};
