import Dialog from '@mui/material/Dialog/Dialog';
import DialogContent from '@mui/material/DialogContent/DialogContent';
import DialogTitle from '@mui/material/DialogTitle/DialogTitle';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { ServiceRecord } from '../../../api/models';
import ReleaseForm from './ReleaseForm';

export interface CreateReleaseDialogProps {
	service: ServiceRecord;
	open: boolean;
	onClose: () => void;
}

const CreateReleaseDialog: React.FC<CreateReleaseDialogProps> = ({
	service,
	open,
	onClose,
}) => {
	const { t } = useTranslation();

	return (
		<Dialog open={open} onClose={onClose}>
			<DialogTitle>{t('release.create.title')}</DialogTitle>
			<DialogContent>
				<ReleaseForm service={service} onSubmitSuccess={onClose} />
			</DialogContent>
		</Dialog>
	);
};

export default CreateReleaseDialog;
