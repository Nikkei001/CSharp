import os
from .xlsx_reader import read_xlsx
from .xls_reader import read_xls

def read_excel_file_custom(file_path):
    """
    读取Excel文件的调度函数。
    根据文件扩展名调用相应的处理函数（read_xlsx 或 read_xls）。
    
    Args:
        file_path (str): Excel文件的完整路径。
        
    Returns:
        list: 包含一个或多个Pandas DataFrame的列表，每个DataFrame对应一个工作表。
              如果文件类型不支持或读取失败，则返回空列表。
    """
    file_name = os.path.basename(file_path)
    if file_name.endswith('.xlsx'):
        return read_xlsx(file_path)
    elif file_name.endswith('.xls'):
        return read_xls(file_path)
    return []