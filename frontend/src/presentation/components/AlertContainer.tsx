import { Grid } from '@mui/material';
import React from 'react';

const AlertContainer: React.FC<{ children: any }> = ({ children }) => {
	return (
		<Grid
			sx={{
				position: 'fixed',
				bottom: 0,
				left: 0,
				right: 0,
				zIndex: 9999,
				pointerEvents: 'none',
				display: 'grid',
			}}
		>
			<Grid
				sx={{
					margin: 'auto auto 0 auto',
					pointerEvents: 'auto',
					width: 'max-content',
				}}
			>
				{children}
			</Grid>
		</Grid>
	);
};

export default AlertContainer;
