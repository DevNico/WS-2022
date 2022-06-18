import Button from '@mui/material/Button/Button';
import Container from '@mui/material/Container/Container';
import Grid from '@mui/material/Grid/Grid';
import Typography from '@mui/material/Typography/Typography';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';
import Center from '../components/Center';

const UnauthorizedPage: React.FC = () => {
	const { t } = useTranslation();

	return (
		<Center>
			<Container maxWidth='sm'>
				<Typography
					variant='h1'
					component='h2'
					textAlign='center'
					sx={{ mb: 2 }}
				>
					{t('unauthorizedPage.title')}
				</Typography>
				<Typography variant='body1' textAlign='center' sx={{ mb: 3 }}>
					{t('unauthorizedPage.subtitle')}
				</Typography>
				<Grid container justifyContent='center'>
					<Button component={Link} to={'/'}>
						{t('notFoundPage.goToHome')}
					</Button>
				</Grid>
			</Container>
		</Center>
	);
};

export default UnauthorizedPage;
