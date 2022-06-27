import Dialog from '@mui/material/Dialog/Dialog';
import DialogContent from '@mui/material/DialogContent/DialogContent';
import DialogTitle from '@mui/material/DialogTitle/DialogTitle';
import React from 'react';
import { useTranslation } from 'react-i18next';
import OrganisationForm from './OrganisationForm';

export interface CreateOrganisationDialogProps {
	open: boolean;
	onClose: () => void;
}

const CreateOrganisationDialog: React.FC<CreateOrganisationDialogProps> = ({
	open,
	onClose,
}) => {
	const { t } = useTranslation();

	return (
		<Dialog open={open} onClose={onClose}>
			<DialogTitle>{t('organisation.create.title')}</DialogTitle>
			<DialogContent>
				<OrganisationForm onSubmitSuccess={onClose} />
			</DialogContent>
		</Dialog>
	);
};

export default CreateOrganisationDialog;
