export type TableColumnData = {
    name: string
    key: string
}

export type TableRowData = {
    [key: string]: any
    style?: React.CSSProperties
    className?: string
    onClick?: (row: any) => void
}

export type TableData = {
    columns: TableColumnData[]
    rows: TableRowData[]
}