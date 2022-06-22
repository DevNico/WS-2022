import Card from '@mui/material/Card/Card';
import CardActionArea from '@mui/material/CardActionArea/CardActionArea';
import CardContent from '@mui/material/CardContent/CardContent';
import Typography from '@mui/material/Typography/Typography';
import React from 'react';
import { OrganisationRecord } from '../../../api/models';

export interface OrganisationCardProps {
	organisation: OrganisationRecord;
}

const OrganisationCard: React.FC<OrganisationCardProps> = ({
	organisation,
}) => {
	return (
		<Card variant='outlined'>
			<CardActionArea>
				<CardContent>
					<Typography variant='h5' gutterBottom>
						{organisation.name!}
					</Typography>
					<Typography variant='caption'>
						{organisation.routeName!}
					</Typography>
				</CardContent>
			</CardActionArea>
		</Card>
	);
};

export default OrganisationCard;
