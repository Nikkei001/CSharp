import os
import pandas as pd
from .excel_reader import read_excel_file_custom

def process_folder(folder_path: str, required_columns: list) -> list:
    """
    遍历指定文件夹中的所有Excel文件，进行数据处理和筛选，并返回一个包含
    所有处理过的、非空DataFrame的列表。

    Args:
        folder_path (str): 要处理的Excel文件所在的文件夹路径。
        required_columns (list): 需要从每个文件中提取的列名列表。

    Returns:
        list: 一个包含所有经过筛选和处理的Pandas DataFrame的列表。
    """
    all_filtered_data = []
    for file_name in os.listdir(folder_path):
        file_path = os.path.join(folder_path, file_name)
        if not (file_name.endswith(('.xls', '.xlsx'))) or file_name.startswith('~'):
            continue

        try:
            print(f'--> 开始处理文件: {file_name}')
            sales_person_name = os.path.splitext(file_name)[0]
            
            sheets_dfs = read_excel_file_custom(file_path)
            
            for df in sheets_dfs:
                if df.empty:
                    continue
                
                df.columns = df.columns.astype(str).str.strip().str.replace(r'\s+', '', regex=True)
                
                columns_to_keep = [col for col in required_columns if col in df.columns]
                if len(columns_to_keep) != len(required_columns):
                    missing = set(required_columns) - set(df.columns)
                    print(f"    - 警告: 在工作表中找不到以下列: {list(missing)}，已跳过。")
                    continue
                
                df = df[columns_to_keep].copy()

                if '辅助列-排序' in df.columns:
                    df['辅助列-排序'] = df['辅助列-排序'].apply(lambda x: x.strip() if isinstance(x, str) else x)

                df['销售确认发货'] = pd.to_numeric(df['销售确认发货'], errors='coerce')
                df = df.dropna(subset=['销售确认发货'])
                filtered_df = df[df['销售确认发货'] > 0].copy()

                if not filtered_df.empty:
                    sales_column = filtered_df['销售'].astype(str).str.strip()
                    if file_name == "周敏.xls":
                        values_to_keep = ['周敏', '待定', '#N/A']
                        filtered_df = filtered_df[sales_column.isin(values_to_keep)].copy()
                    else:
                        filtered_df = filtered_df[sales_column == sales_person_name].copy()

                if not filtered_df.empty:
                    all_filtered_data.append(filtered_df)

            print(f'--> 文件 {file_name} 处理完成')

        except Exception as e:
            print(f'处理文件 {file_name} 时发生严重错误: {e}')
    
    return all_filtered_data
print("-" * 20)