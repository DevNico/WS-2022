import { CssBaseline, ThemeProvider } from '@mui/material';
import React from 'react';
import { BrowserRouter } from 'react-router-dom';
import Router from './Router';
import theme from './theme';
import './i18n';
import { QueryClient, QueryClientProvider } from 'react-query';
import { ReactQueryDevtools } from 'react-query/devtools';

const queryClient = new QueryClient();

export const Root: React.FC = () => {
	return (
		<BrowserRouter>
			<ThemeProvider theme={theme}>
				<QueryClientProvider client={queryClient}>
					{/* CssBaseline kickstart an elegant, consistent, and simple baseline to build upon. */}
					<CssBaseline />
					<Router />
					<ReactQueryDevtools initialIsOpen={false} />
				</QueryClientProvider>
			</ThemeProvider>
		</BrowserRouter>
	);
};
