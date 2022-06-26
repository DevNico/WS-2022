import Box from '@mui/material/Box/Box';
import MuiDrawer from '@mui/material/Drawer';
import React from 'react';
import { useRecoilState } from 'recoil';
import { drawerOpenState } from '../../store/generalState';

export const drawerWidth = 240;

interface BaseDrawerProps {
	children: (handleDrawerClose: () => void) => React.ReactNode;
}

const BaseDrawer: React.FC<BaseDrawerProps> = ({ children }) => {
	const [drawerOpen, setDrawerOpen] = useRecoilState(drawerOpenState);

	const handleDrawerToggle = () => {
		setDrawerOpen((v) => !v);
	};

	const drawer = children(handleDrawerToggle);

	return (
		<>
			<Box
				component='nav'
				sx={{
					display: 'flex',
					width: { sm: drawerWidth },
					flexShrink: { sm: 0 },
				}}
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
		</>
	);
};

export default BaseDrawer;
