import { red } from '@mui/material/colors';
import { createTheme } from '@mui/material/styles';

const theme = createTheme({
	palette: {
		primary: {
			main: '#556cd6',
		},
		secondary: {
			main: '#19857b',
		},
		error: {
			main: red.A400,
		},
	},
	typography: {
		h1: {
			fontSize: '3rem',
		},
		h2: {
			fontSize: '2rem',
		},
		h3: {
			fontSize: '1.5rem',
		},
		body1: {
			fontSize: '1.2rem',
		},
		body2: {
			fontSize: '1rem',
		},
		button: {
			textTransform: 'none',
		},
	},
	components: {
		MuiButton: {
			defaultProps: {
				variant: 'contained',
			},
		},
		MuiDialog: {
			defaultProps: {
				maxWidth: 'sm',
				fullWidth: true,
			},
		},
		MuiListItem: {
			defaultProps: {
				dense: true,
			},
		},
	},
});

export default theme;
