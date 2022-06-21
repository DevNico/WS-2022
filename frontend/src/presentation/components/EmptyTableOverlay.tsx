import React from 'react';
import { Button, Stack, Typography, Grid } from '@mui/material';
import { useNavigate } from 'react-router-dom';

interface EmptyTableOverlayProps {
	text: string;
	buttonText: string;
	target: string;
}

const EmptyTableOverlay: React.FC<EmptyTableOverlayProps> = ({
	text,
	buttonText,
	target,
}) => {
	const navigate = useNavigate();

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
				<Button
					onClick={() => navigate(target)}
					sx={{
						zIndex: (theme) => theme.zIndex.drawer + 1,
						width: 'max-content',
						alignSelf: 'center',
					}}
				>
					{buttonText}
				</Button>
			</Stack>
		</Grid>
	);
};

export default EmptyTableOverlay;
