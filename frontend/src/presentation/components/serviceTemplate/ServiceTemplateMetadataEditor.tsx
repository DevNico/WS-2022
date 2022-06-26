import Editor, { BeforeMount, OnChange } from '@monaco-editor/react';
import { Typography } from '@mui/material';
import Paper from '@mui/material/Paper';
import Stack from '@mui/material/Stack';
import React from 'react';

export interface ServiceTemplateMetadataEditorProps {
	label: string;
	value: string;
	onChanged: (newValue: string) => void;
}

const ServiceTemplateMetadataEditor: React.FC<
	ServiceTemplateMetadataEditorProps
> = ({ label, value, onChanged }) => {
	const handleChange: OnChange = (value, _) => {
		onChanged(value ?? '');
	};

	const handleBeforeMount: BeforeMount = (monaco) => {
		monaco.languages.json.jsonDefaults.setDiagnosticsOptions({
			validate: true,
			allowComments: false,
			schemas: [
				// {
				// 	uri: 'https://server/schema.json',
				// 	fileMatch: ['*.json'],
				// 	schema: schema,
				// },
			],
		});
	};

	return (
		<Paper variant='outlined'>
			<Stack spacing={[1, 2]}>
				<Typography m={2}>{label}</Typography>
				<Editor
					wrapperProps={{
						id: 'static-metadata',
						name: 'static-metadata',
					}}
					options={{
						scrollBeyondLastLine: false,
					}}
					beforeMount={handleBeforeMount}
					height='500px'
					language='json'
					value={value}
					onChange={handleChange}
				/>
			</Stack>
		</Paper>
	);
};

export default ServiceTemplateMetadataEditor;
