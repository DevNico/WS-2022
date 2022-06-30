import React from 'react';
import { Button, Stack } from '@mui/material';
import TabPanel from '../TabPanel';
import { useTranslation } from 'react-i18next';
import AddIcon from '@mui/icons-material/Add';
import AddLocaleDialog from './AddLocaleDialog';
import { LocaleRecord, MetadataArrayElement } from '../../../api/models';
import LocalizedMetadataCard from './LocalizedMetadataCard';

interface LocalizedMetadataTabPanelProps {
	index: number;
	value: number;
	loading: boolean;
	metadata: MetadataArrayElement[];
	locales: LocaleRecord[];
	onLocaleAdd: (locale: LocaleRecord) => void;
	onLocaleDelete: (locale: LocaleRecord) => void;
}

const LocalizedMetadataTabPanel: React.FC<LocalizedMetadataTabPanelProps> = ({
	metadata,
	locales,
	index,
	value,
	loading,
	onLocaleAdd,
	onLocaleDelete,
}) => {
	const [localeDialogOpen, setLocaleDialogOpen] = React.useState(false);
	const [localesCopy, setLocalesCopy] = React.useState(locales);
	const [selectedLocales, setSelectedLocales] = React.useState<
		LocaleRecord[]
	>([]);
	const { t } = useTranslation();

	if (selectedLocales.length === 0 && localesCopy.length !== locales.length) {
		setLocalesCopy(locales);
	}

	const handleAddLocale = (localeId?: number) => {
		setLocaleDialogOpen(false);
		const locale = locales.find((l) => l.id === localeId);
		setLocalesCopy(localesCopy.filter((l) => l.id !== localeId));

		if (locale) {
			setSelectedLocales([...selectedLocales, locale]);
			onLocaleAdd(locale);
		}
	};

	const handleLocaleDelete = (locale: LocaleRecord) => {
		setLocalesCopy([...localesCopy, locale]);
		setSelectedLocales(selectedLocales.filter((l) => l.id !== locale.id));
		onLocaleDelete(locale);
	};

	return (
		<>
			<TabPanel value={value} index={index}>
				<Stack spacing={2} justifyContent='center' alignItems='center'>
					{selectedLocales.map((locale) => (
						<LocalizedMetadataCard
							key={locale.id}
							metadata={metadata}
							locale={locale}
							onDelete={handleLocaleDelete.bind(null, locale)}
						/>
					))}
					<Button
						startIcon={<AddIcon />}
						variant='text'
						onClick={() => setLocaleDialogOpen(true)}
						disabled={loading || localesCopy.length === 0}
					>
						{t('release.create.addLocalizedMetadata')}
					</Button>
				</Stack>
			</TabPanel>
			<AddLocaleDialog
				locales={localesCopy}
				open={localeDialogOpen}
				onClose={handleAddLocale}
			/>
		</>
	);
};

export default LocalizedMetadataTabPanel;
