module.exports = {
	srm: {
		output: {
			mode: 'tags-split',
			target: 'src/api/apiClient.ts',
			schemas: 'src/api/models',
			client: 'react-query',
			mock: false,
			override: {
				mutator: {
					path: './src/api/axios.ts',
					name: 'customInstance',
				},
			},
		},
		input: {
			target: './src/api/schema.json',
		},
	},
};
