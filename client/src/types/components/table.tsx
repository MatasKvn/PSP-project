export type TableColumnData = {
    name: string
    key: string
}

export type TableRowData = {
    [key: string]: any
}

export type TableData = {
    columns: TableColumnData[]
    rows: TableRowData[]
}