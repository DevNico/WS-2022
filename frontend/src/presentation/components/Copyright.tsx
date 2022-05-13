import Link from '@mui/material/Link/Link';
import Typography from '@mui/material/Typography/Typography';
import React from 'react';

const Copyright: React.FC = () => {
	return (
		<Typography variant='body2' color='text.secondary' align='center'>
			{'Copyright © '}
			<Link color='inherit' href='https://mui.com/'>
				Service Release Manager
			</Link>{' '}
			{new Date().getFullYear()}
			{'.'}
		</Typography>
	);
};

export default Copyright;
