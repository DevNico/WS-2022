import Card from '@mui/material/Card/Card';
import CardActionArea from '@mui/material/CardActionArea/CardActionArea';
import CardContent from '@mui/material/CardContent/CardContent';
import Stack from '@mui/material/Stack';
import Typography from '@mui/material/Typography/Typography';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { OrganisationRecord } from '../../../api/models';
import { homeRoute } from '../../Router';
import RouterCardActionArea from '../RouterCardActionArea';

export interface OrganisationCardProps {
	organisation: OrganisationRecord;
}

const OrganisationCard: React.FC<OrganisationCardProps> = ({
	organisation,
}) => {
	const { t } = useTranslation();

	return (
		<Card variant='outlined'>
			<RouterCardActionArea
				to={homeRoute({}).organisation({
					name: organisation.routeName!,
				})}
			>
				<CardContent>
					<Stack>
						<Typography variant='h5' gutterBottom>
							{organisation.name!}
						</Typography>

						<Typography variant='caption'>
							{organisation.routeName!}
						</Typography>
						<Typography variant='caption'>
							{t('organisation.card.updatedAt', {
								updatedAt: new Date(
									organisation.createdAt!
								).toLocaleDateString(),
								interpolation: { escapeValue: false },
							})}
						</Typography>
					</Stack>
				</CardContent>
			</RouterCardActionArea>
		</Card>
	);
};

export default OrganisationCard;
