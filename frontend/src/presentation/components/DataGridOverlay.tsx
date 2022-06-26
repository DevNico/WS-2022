import { Button, Grid, Stack, Typography } from '@mui/material';
import React from 'react';

interface DataGridOverlayOverlayProps {
	text: string;
	buttonText?: string;
	onClick?: () => void;
}

const DataGridOverlay: React.FC<DataGridOverlayOverlayProps> = ({
	text,
	buttonText,
	onClick,
}) => {
	return (
		<Grid sx={{ width: '100%', height: '100%' }} container>
			<Stack
				spacing={2}
				justifyContent='center'
				justifyItems='center'
				margin='auto'
				width='max-content'
				sx={{
					padding: '10px 20px',
					borderRadius: '5px',
					border: '1px solid #e0e0e0',
				}}
			>
				<Typography textAlign='center'>{text}</Typography>
				{buttonText && (
					<Button
						onClick={onClick}
						sx={{
							zIndex: (theme) => theme.zIndex.drawer + 1,
							width: 'max-content',
							alignSelf: 'center',
						}}
					>
						{buttonText}
					</Button>
				)}
			</Stack>
		</Grid>
	);
};

export default DataGridOverlay;
