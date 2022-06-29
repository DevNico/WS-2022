import React from 'react';
import {
	Card,
	CardActions,
	CardContent,
	IconButton,
	Stack,
	Tooltip,
	Typography,
} from '@mui/material';
import { LocaleRecord, MetadataArrayElement } from '../../../api/models';
import { useTranslation } from 'react-i18next';
import { Delete } from '@mui/icons-material';
import GeneratedServiceTemplateForm from './GeneratedServiceTemplateForm';

interface LocalizedMetadataCardProps {
	metadata: MetadataArrayElement[];
	locale: LocaleRecord;
	onDelete: () => void;
}

const LocalizedMetadataCard: React.FC<LocalizedMetadataCardProps> = ({
	metadata,
	locale,
	onDelete,
}) => {
	const { t } = useTranslation();

	return (
		<Card variant='outlined'>
			<CardContent>
				<Stack spacing={2} alignItems='center' justifyContent='center'>
					<Typography>
						{t('release.create.localizedMetadataFor', {
							locale: locale.tag,
						})}
					</Typography>
					<GeneratedServiceTemplateForm
						template={metadata}
						prefix={'locale-' + locale.tag}
					/>
				</Stack>
			</CardContent>
			<CardActions sx={{ alignItems: 'right', justifyContent: 'right' }}>
				<Tooltip title={t('common.delete')}>
					<IconButton onClick={onDelete}>
						<Delete />
					</IconButton>
				</Tooltip>
			</CardActions>
		</Card>
	);
};

export default LocalizedMetadataCard;
