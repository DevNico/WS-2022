export function dateValueFormatter(params: any) {
	return new Date(params.value).toLocaleDateString();
}
