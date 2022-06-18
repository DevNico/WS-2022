import { AccountCircle } from '@mui/icons-material';
import CorporateFareIcon from '@mui/icons-material/CorporateFare';
import MailIcon from '@mui/icons-material/Mail';
import MenuIcon from '@mui/icons-material/Menu';
import PersonAddIcon from '@mui/icons-material/PersonAdd';
import InboxIcon from '@mui/icons-material/MoveToInbox';
import MuiAppBar from '@mui/material/AppBar/AppBar';
import Box from '@mui/material/Box/Box';
import Divider from '@mui/material/Divider/Divider';
import MuiDrawer from '@mui/material/Drawer';
import IconButton from '@mui/material/IconButton/IconButton';
import List from '@mui/material/List/List';
import ListItem from '@mui/material/ListItem/ListItem';
import ListItemButton from '@mui/material/ListItemButton/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon/ListItemIcon';
import ListItemText from '@mui/material/ListItemText/ListItemText';
import Menu from '@mui/material/Menu/Menu';
import MenuItem from '@mui/material/MenuItem/MenuItem';
import styled from '@mui/material/styles/styled';
import Toolbar from '@mui/material/Toolbar/Toolbar';
import Typography from '@mui/material/Typography/Typography';
import { useKeycloak } from '@react-keycloak/web';
import React, { useCallback, useEffect } from 'react';
import Div100vh from 'react-div-100vh';
import { useTranslation } from 'react-i18next';
import { Outlet, useLocation, useNavigate } from 'react-router-dom';
import { atom, useRecoilState, useSetRecoilState } from 'recoil';

const Root = styled(Div100vh)`
	display: flex;
	flex-direction: column;
	flex: 0% 1 1;
	overflow: hidden;
`;
const Main = styled('div')`
	flex-grow: 1;
	align-items: stretch;
	display: flex;
	flex-basis: auto;
	flex-direction: row;
`;
const Content = styled(Box)`
	flex-grow: 1;
	overflow: auto;
`;

const drawerOpenState = atom({
	key: 'drawerOpenState',
	default: false,
});

const drawerWidth = 240;

const AppBar: React.FC = () => {
	const { keycloak } = useKeycloak();
	const { t } = useTranslation();

	const [anchorEl, setAnchorEl] = React.useState(null);
	const openUserMenu = (event: any) => setAnchorEl(event.currentTarget);
	const closeUserMenu = () => setAnchorEl(null);

	const setDrawerOpen = useSetRecoilState(drawerOpenState);
	const handleDrawerToggle = () => setDrawerOpen((v) => !v);

	const logout = useCallback(() => {
		keycloak?.logout();
		closeUserMenu();
	}, [keycloak]);

	return (
		<MuiAppBar position='relative'>
			<Toolbar>
				<IconButton
					color='inherit'
					aria-label='open drawer'
					edge='start'
					onClick={handleDrawerToggle}
					sx={{ mr: 2, display: { sm: 'none' } }}
				>
					<MenuIcon />
				</IconButton>
				<Typography variant='h6' component='div' sx={{ flexGrow: 1 }}>
					Service Release Manager
				</Typography>
				{keycloak.authenticated && (
					<div>
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
							<MenuItem onClick={logout}>{t('logout')}</MenuItem>
						</Menu>
					</div>
				)}
			</Toolbar>
		</MuiAppBar>
	);
};

const Drawer = () => {
	const [drawerOpen, setDrawerOpen] = useRecoilState(drawerOpenState);
	const [isSuperAdmin, setIsSuperAdmin] = React.useState(false);
	const { keycloak } = useKeycloak();

	useEffect(() => {
		if (keycloak.authenticated) {
			keycloak.loadUserInfo().then(() => {
				setIsSuperAdmin(keycloak.hasRealmRole('superAdmin'));
			});
		}
	}, [keycloak.authenticated]);

	const handleDrawerToggle = () => {
		setDrawerOpen((v) => !v);
	};

	const navigate = useNavigate();
	const { t } = useTranslation();

	const location = useLocation();
	const isOrganisationsPage = location.pathname === '/organisations';
	const isUsersCreatePage = location.pathname === '/users/create';
	const isRolesCreatePage = location.pathname === '/roles/create';

	const drawer = (
		<div>
			<List>
				<ListItem disablePadding>
					<ListItemButton
						selected={isOrganisationsPage}
						onClick={() => navigate('/organisations')}
					>
						<ListItemIcon>
							<CorporateFareIcon />
						</ListItemIcon>
						<ListItemText primary={t('homeLayout.organisations')} />
					</ListItemButton>
				</ListItem>
				{isSuperAdmin && (
					<ListItem disablePadding>
						<ListItemButton
							selected={isUsersCreatePage}
							onClick={() => navigate('/users/create')}
						>
							<ListItemIcon>
								<PersonAddIcon />
							</ListItemIcon>
							<ListItemText primary={t('homeLayout.users')} />
						</ListItemButton>
					</ListItem>
				)}
				{isSuperAdmin && (
					<ListItem disablePadding>
						<ListItemButton
							selected={isRolesCreatePage}
							onClick={() => navigate('/roles/create')}
						>
							<ListItemIcon>
								<PersonAddIcon />
							</ListItemIcon>
							<ListItemText primary={t('homeLayout.roles')} />
						</ListItemButton>
					</ListItem>
				)}
			</List>
			<Divider />
			<List>
				<Typography variant='h6' mx={2}>
					Services
				</Typography>
				{['All mail', 'Trash', 'Spam'].map((text, index) => (
					<ListItem key={text} disablePadding>
						<ListItemButton>
							<ListItemIcon>
								{index % 2 === 0 ? <InboxIcon /> : <MailIcon />}
							</ListItemIcon>
							<ListItemText primary={text} />
						</ListItemButton>
					</ListItem>
				))}
			</List>
		</div>
	);

	return (
		<Box
			component='nav'
			sx={{
				display: 'flex',
				width: { sm: drawerWidth },
				flexShrink: { sm: 0 },
			}}
			aria-label='mailbox folders'
		>
			{/* The implementation can be swapped with js to avoid SEO duplication of links. */}
			<MuiDrawer
				variant='temporary'
				open={drawerOpen}
				onClose={handleDrawerToggle}
				ModalProps={{
					keepMounted: true, // Better open performance on mobile.
				}}
				sx={{
					display: { xs: 'block', sm: 'none' },
					flexGrow: 1,
					'& .MuiDrawer-paper': {
						boxSizing: 'border-box',
						width: drawerWidth,
						position: 'relative',
					},
				}}
			>
				{drawer}
			</MuiDrawer>
			<MuiDrawer
				variant='permanent'
				sx={{
					display: { xs: 'none', sm: 'block' },
					flexGrow: 1,
					'& .MuiDrawer-paper': {
						boxSizing: 'border-box',
						width: drawerWidth,
						position: 'relative',
					},
				}}
				open
			>
				{drawer}
			</MuiDrawer>
		</Box>
	);
};

const HomeLayout: React.FC = () => {
	return (
		<Root>
			<AppBar />
			<Main>
				<Drawer />
				<Content p={[2, 4]}>
					<Outlet />
				</Content>
			</Main>
		</Root>
	);
};

export default HomeLayout;
