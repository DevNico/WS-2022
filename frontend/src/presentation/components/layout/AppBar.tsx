import { AccountCircle } from '@mui/icons-material';
import MenuIcon from '@mui/icons-material/Menu';
import MuiAppBar from '@mui/material/AppBar/AppBar';
import IconButton from '@mui/material/IconButton/IconButton';
import Menu from '@mui/material/Menu/Menu';
import MenuItem from '@mui/material/MenuItem/MenuItem';
import Toolbar from '@mui/material/Toolbar/Toolbar';
import { useKeycloak } from '@react-keycloak/web';
import React, { useCallback } from 'react';
import { useTranslation } from 'react-i18next';
import { useRecoilValue, useSetRecoilState } from 'recoil';
import { homeRoute } from '../../Router';
import { drawerOpenState } from '../../store/generalState';
import { isSuperAdminState } from '../../store/keycloakState';
import RouterButton from '../RouterButton';
import RouterLink from '../RouterLink';
import { useDrawer } from './Drawer';

const HomeAppBar: React.FC = () => {
	const { keycloak } = useKeycloak();
	const { t } = useTranslation();

	const [anchorEl, setAnchorEl] = React.useState(null);
	const openUserMenu = (event: any) => setAnchorEl(event.currentTarget);
	const closeUserMenu = () => setAnchorEl(null);

	const setDrawerOpen = useSetRecoilState(drawerOpenState);
	const handleDrawerToggle = () => setDrawerOpen((v) => !v);

	const isSuperAdmin = useRecoilValue(isSuperAdminState);

	const logout = useCallback(() => {
		keycloak?.logout();
		closeUserMenu();
	}, [keycloak]);

	const drawer = useDrawer();

	return (
		<MuiAppBar position='relative' elevation={0}>
			<Toolbar>
				{drawer && (
					<IconButton
						color='inherit'
						aria-label='open drawer'
						edge='start'
						onClick={handleDrawerToggle}
						sx={{ mr: 2, display: { sm: 'none' } }}
					>
						<MenuIcon />
					</IconButton>
				)}
				<RouterLink to={homeRoute({})} variant='h6' color='#FFFFFF'>
					Service Release Manager
				</RouterLink>
				<div style={{ flexGrow: 1 }} />
				{isSuperAdmin && (
					<RouterButton
						variant='text'
						to={homeRoute({}).admin({})}
						sx={{
							color: 'white',
						}}
					>
						{t('appbar.admin')}
					</RouterButton>
				)}
				{keycloak.authenticated && (
					<>
						<IconButton
							size='large'
							aria-label='account of current user'
							aria-controls='menu-appbar'
							aria-haspopup='true'
							onClick={openUserMenu}
							color='inherit'
						>
							<AccountCircle />
						</IconButton>
						<Menu
							id='menu-appbar'
							anchorEl={anchorEl}
							anchorOrigin={{
								vertical: 'bottom',
								horizontal: 'right',
							}}
							keepMounted
							transformOrigin={{
								vertical: 'top',
								horizontal: 'right',
							}}
							open={Boolean(anchorEl)}
							onClose={closeUserMenu}
						>
							<MenuItem onClick={logout}>
								{t('appbar.usermenu.logout')}
							</MenuItem>
						</Menu>
					</>
				)}
			</Toolbar>
		</MuiAppBar>
	);
};

export default HomeAppBar;
