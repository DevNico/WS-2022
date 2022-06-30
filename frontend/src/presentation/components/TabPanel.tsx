import React from 'react';
import { Box, Typography } from '@mui/material';

interface TabPanelProps {
	children: any;
	value: number;
	index: number;
}

function a11yProps(index: number) {
	return {
		id: `tab-${index}`,
		'aria-controls': `tabpanel-${index}`,
	};
}

const TabPanel: React.FC<TabPanelProps> = ({ children, index, value }) => {
	return (
		<div
			role='tabpanel'
			hidden={value !== index}
			aria-labelledby={`tab-${index}`}
			{...a11yProps(index)}
		>
			{value === index && <Box sx={{ p: 3 }}>{children}</Box>}
		</div>
	);
};

export default TabPanel;
