import React, { useCallback } from 'react';
import {
	AppBar,
	Button,
	IconButton,
	Menu,
	MenuItem,
	Toolbar,
	Typography,
} from '@mui/material';
import { AccountCircle } from '@mui/icons-material';
import MenuIcon from '@mui/icons-material/Menu';
import { useKeycloak } from '@react-keycloak/web';
import { useTranslation } from 'react-i18next';

const CustomAppBar: React.FC = () => {
	const [anchorEl, setAnchorEl] = React.useState(null);
	const { keycloak } = useKeycloak();
	const { t } = useTranslation();

	const handleMenu = (event: any) => {
		setAnchorEl(event.currentTarget);
	};

	const handleClose = () => {
		setAnchorEl(null);
	};

	const login = useCallback(() => {
		keycloak?.login();
	}, [keycloak]);

	const logout = useCallback(() => {
		keycloak?.logout();
		handleClose();
	}, [keycloak]);

	return (
		<AppBar position='static'>
			<Toolbar>
				<IconButton
					size='large'
					edge='start'
					color='inherit'
					aria-label='menu'
					sx={{ mr: 2 }}
				>
					<MenuIcon />
				</IconButton>
				<Typography variant='h6' component='div' sx={{ flexGrow: 1 }}>
					Service Release Manager
				</Typography>
				{(keycloak.authenticated && (
					<div>
						<IconButton
							size='large'
							aria-label='account of current user'
							aria-controls='menu-appbar'
							aria-haspopup='true'
							onClick={handleMenu}
							color='inherit'
						>
							<AccountCircle />
						</IconButton>
						<Menu
							id='menu-appbar'
							anchorEl={anchorEl}
							anchorOrigin={{
								vertical: 'top',
								horizontal: 'right',
							}}
							keepMounted
							transformOrigin={{
								vertical: 'top',
								horizontal: 'right',
							}}
							open={Boolean(anchorEl)}
							onClose={handleClose}
						>
							<MenuItem onClick={logout}>{t('logout')}</MenuItem>
						</Menu>
					</div>
				)) || <Button onClick={login}>{t('login')}</Button>}
			</Toolbar>
		</AppBar>
	);
};

export default CustomAppBar;
