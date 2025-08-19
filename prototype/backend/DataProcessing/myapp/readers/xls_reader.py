import pandas as pd
import xlrd
import os
from xlrd.biffh import XLRDError

def read_xls(file_path):
    """专门处理 .xls 文件，保留公式错误。"""
    sheets_dfs = []
    file_name = os.path.basename(file_path)
    try:
        book = xlrd.open_workbook(file_path, on_demand=True)
        for sheet_name in book.sheet_names():
            sheet = book.sheet_by_name(sheet_name)
            if sheet.nrows < 4: continue
            header = [sheet.cell_value(3, col) for col in range(sheet.ncols)]
            data_rows = []
            for rx in range(4, sheet.nrows):
                row_values = []
                for cx in range(sheet.ncols):
                    cell = sheet.cell(rx, cx)
                    if cell.ctype == xlrd.XL_CELL_ERROR:
                        error_text = xlrd.error_text_from_code.get(cell.value, '#ERROR?')
                        row_values.append(error_text)
                    else:
                        row_values.append(cell.value)
                data_rows.append(row_values)
            
            if data_rows:
                sheets_dfs.append(pd.DataFrame(data_rows, columns=header))
    except XLRDError as e:
        print(f"    - 警告: 无法处理 .xls 文件 '{file_name}' (可能已损坏或格式不受支持): {e}")
    return sheets_dfs